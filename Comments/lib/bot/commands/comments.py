# Imports
import discord, mysql.connector, os, subprocess
from discord.ext import commands
from discord.commands import Option
from discord.commands import SlashCommandGroup
from dotenv import load_dotenv
from table2ascii import table2ascii as t2a
from lib.encryption import decrypt
from lib.bot.functions import send_embed, send_embed_error, MySQL_CheckIfTableExists, MySQL_CheckIfRowExists, LogAndPrint

# Load .env
load_dotenv()

# Variables
sql_config = {
    'user': os.getenv("MYSQL_DB_USER"),
    'password': os.getenv("MYSQL_DB_PSWD"),
    'host': 'localhost'
}
DISCORD_ROLE_CommentsRW = int(os.getenv("DISCORD_ROLE_COMMENTSRW"))
API_Port = os.getenv("API_PORT")
API_PORT_ALT = os.getenv("API_PORT_ALT")
DB_Comments_Public = os.getenv("DB_COMMENTS_PUBLIC")
DB_Comments_API = os.getenv("DB_COMMENTS_API")


# Comments Cog
class comments(commands.Cog):
    def __init__(self, bot: discord.Bot):  # Make the Bot Variable accessible in Cogs
        self.bot = bot

    # Create "/comments" Slash Command Group
    commentsgrp = SlashCommandGroup("comments", "Manage ViVeTool Comments")

    # Bot /comments get command. Returns a Table of Comments for a specified Build
    @commentsgrp.command(name="get", description="Returns a Table of every Feature Name and Comment of a desired Build")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def get(self, ctx: discord.ApplicationContext, build: Option(int, "Enter a Build Number", required=True)):
        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
        DB_Cursor = DB_Connection.cursor()

        # Check if a Table for the specified Build exists
        Exists = await MySQL_CheckIfTableExists(build)
        if Exists is False:
            await send_embed_error(ctx, f"No comment for Build {build} currently exists",
                                   "Use /comments add to add a comment", UseFollowUpSend=True)
            LogAndPrint("Tried to get a comment list for a Build that doesn't exist", "warning")
            return

        # Get all Comments
        DB_Cursor.execute(f"SELECT * FROM {DB_Comments_Public}.`{build}`")
        DB_comments = DB_Cursor.fetchall()

        # Create a List
        DB_commentslist = [["", ""]]

        # Append every Comment to the List as a nested List
        for row in DB_comments:
            DB_commentslist.append([row[0], row[1]])

        # Use Table2Ascii to convert the List to an ASCII Table
        output = t2a(
            header=["Feature Name", "Comment"],
            body=DB_commentslist,
            first_col_heading=True
        )

        # Return the Table within Quote Blocks
        await ctx.followup.send(f"```\n{output}\n```")

        # Close the DB Connection
        DB_Cursor.close()
        DB_Connection.close()

    # Bot /comments add command. Adds a ViVeTool Comment to the Database
    @commentsgrp.command(name="add", description="Adds a ViVeTool Comment to the Database")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def add(self, ctx: discord.ApplicationContext, build: Option(int, "Enter a Build Number", required=True),
                  featurename: Option(str, "Enter a ViVeTool Feature Name that you want to add", required=True),
                  comment: Option(str, "Enter a ViVeTool Comment", required=True)):
        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
        DB_Cursor = DB_Connection.cursor()

        # Create the MySQL Table if it doesn't exist
        Exists = await MySQL_CheckIfTableExists(build)
        if Exists is False:
            DB_Cursor.execute(
                f"CREATE TABLE {DB_Comments_Public}.`{build}` (FeatureName varchar(100) NOT NULL COLLATE 'utf8mb4_general_ci', Comment TEXT NOT NULL COLLATE 'utf8mb4_general_ci')")
            await send_embed(ctx, f"Successfully created a Database Table for Build {build}", discord.Color.green(), UseRespond=True)
            LogAndPrint(f"Created MySQL Table {build}", "info")

        # Check if a comment already exists for the specified Feature
        if await MySQL_CheckIfRowExists(f"{DB_Comments_Public}.`{build}`", "FeatureName", f'"{featurename}"') is True:
            await send_embed_error(ctx, f"A comment for {featurename} in {build} already exists",
                                   "Use /comments edit to edit a comment", UseRespond=True)
            LogAndPrint("Tried to add a comment to a Feature that already has one", "warning")
            return

        # Insert Values in the Table
        DB_Cursor.execute(
            f'INSERT INTO {{}}.`{{}}` (FeatureName, Comment) VALUES ("{{}}", "{{}}");'.format(DB_Comments_Public, build,
                                                                                              featurename, comment))
        DB_Cursor.close()
        await send_embed(ctx, f'The following comment was successfully added to {featurename} in Build {build}:',
                         discord.Color.green(), f"{comment}", UseRespond=True)
        LogAndPrint(f"A comment for {featurename} in {build} was added to the DB.", "info")

        # Commit to the Database and close the connection
        DB_Connection.commit()
        DB_Connection.close()

    # Bot /comments edit command. Edits a ViVeTool Comment on the Database
    @commentsgrp.command(name="edit", description="Edits an existing ViVeTool Comment in the Database")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def edit(self, ctx, build: Option(int, "Enter a Build Number", required=True),
                   featurename: Option(str, "Enter a ViVeTool Feature Name that you want to edit", required=True),
                   comment: Option(str, "Enter a new ViVeTool Comment", required=True)):
        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
        DB_Cursor = DB_Connection.cursor()

        # Create the MySQL Table if it doesn't exist
        Exists = await MySQL_CheckIfTableExists(build)
        if Exists is False:
            await send_embed_error(ctx, f"No comment for Build {build} was ever created", UseRespond=True)
            LogAndPrint(f"Tried to edit a comment for Build {build} but the MySQL Table doesn't exist", "warning")
            return

        # Check if no comment exists for the specified Feature
        if await MySQL_CheckIfRowExists(f"{DB_Comments_Public}.`{build}`", "FeatureName", f'"{featurename}"') is False:
            await send_embed_error(ctx, f"A comment for {featurename} in {build} doesn't exist.",
                                   "Use /comments add to add a comment", UseRespond=True)
            LogAndPrint("Tried to edit a comment for a Feature that doesn't exist in the DB", "warning")
            return

        # Update Values in the Table
        DB_Cursor.execute(
            f'UPDATE {DB_Comments_Public}.`{build}` SET Comment = "{comment}" WHERE FeatureName = "{featurename}";')
        DB_Cursor.close()
        await send_embed(ctx, f'The comment for {featurename} in Build {build} was successfully updated to:',
                         discord.Color.green(), f"{comment}", UseRespond=True)
        LogAndPrint(f"A comment for {featurename} in {build} was updated in the DB.", "info")

        # Commit to the Database and close the connection
        DB_Connection.commit()
        DB_Connection.close()

    # Bot /comments copy command. Copies a ViVeTool Comment on the Database to a new Build
    @commentsgrp.command(name="copy", description="Copies an existing ViVeTool Comment in the Database to a new Build")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def copy(self, ctx, oldbuild: Option(int, "Enter the Build Number you want to copy from", required=True),
                   featurename: Option(str, "Enter a ViVeTool Feature Name that you want to copy", required=True),
                   newbuild: Option(int, "Enter the Build Number you want to copy to", required=True)):

        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Check if oldbuild and newbuild are the same
        if oldbuild == newbuild:
            await send_embed_error(ctx, f"Incorrect Parameters. Both oldbuild and newbuild are {newbuild}", UseRespond=True)
            LogAndPrint(f"Tried to copy Data from Table {oldbuild} to {newbuild}", "warning")
            return

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
        DB_Cursor = DB_Connection.cursor()

        # Check if the Old Table Exists
        Exists_oldTable = await MySQL_CheckIfTableExists(oldbuild)
        if Exists_oldTable is False:
            await send_embed_error(ctx, f"No comment for Build {oldbuild} was ever created", UseRespond=True)
            LogAndPrint(f"Tried to copy a comment from Build {oldbuild} but the MySQL Table doesn't exist", "warning")
            return

        # Check if no comment exists for the specified Feature
        if await MySQL_CheckIfRowExists(f"{DB_Comments_Public}.`{oldbuild}`", "FeatureName",
                                        f'"{featurename}"') is False:
            await send_embed_error(ctx, f"A comment for {featurename} in {oldbuild} doesn't exist.",
                                   "Use /comments add to add a comment", UseRespond=True)
            LogAndPrint("Tried to copy a comment for a Feature that doesn't exist in the DB", "warning")
            return

        # Check if the New Table Exists
        Exists_newTable = await MySQL_CheckIfTableExists(newbuild)
        if Exists_newTable is False:
            # Create a New Table if it doesn't
            DB_Cursor.execute(
                f"CREATE TABLE {DB_Comments_Public}.`{newbuild}` (FeatureName varchar(100) NOT NULL COLLATE 'utf8mb4_general_ci', Comment TEXT NOT NULL COLLATE 'utf8mb4_general_ci')")
            await send_embed(ctx, f"Successfully created a Database Table for Build {newbuild}", discord.Color.green(), UseRespond=True)
            LogAndPrint(f"Created MySQL Table {newbuild}", "info")

            # Add Values into the new Table
            DB_Cursor.execute(
                f"INSERT INTO {{}}.`{{}}` SELECT * FROM {{}}.`{{}}` WHERE {{}}.`{{}}`.FeatureName='{{}}';".format(DB_Comments_Public, newbuild, DB_Comments_Public, oldbuild,
                                                                                                                  DB_Comments_Public, oldbuild, featurename))
            DB_Cursor.close()

            await send_embed(ctx, f"Successfully copied {featurename} to Build {newbuild}", discord.Color.green(),
                             UseRespond=True)
            LogAndPrint(f"Copied {featurename} to MySQL Table {newbuild}", "info")

            # Commit to the Database and close the connection
            DB_Connection.commit()
            DB_Connection.close()
            return
        else:
            # Add Values into the Table
            DB_Cursor.execute(
                f"INSERT INTO {DB_Comments_Public}.`{newbuild}` SELECT * FROM {DB_Comments_Public}.`{oldbuild}` WHERE {DB_Comments_Public}.`{oldbuild}`.FeatureName='{featurename}';")
            DB_Cursor.close()

            await send_embed(ctx, f"Successfully copied {featurename} to Build {newbuild}", discord.Color.green(),
                             UseRespond=True)
            LogAndPrint(f"Copied {featurename} to MySQL Table {newbuild}", "info")

            # Commit to the Database and close the connection
            DB_Connection.commit()
            DB_Connection.close()
            return

    # Bot /comments copyall command. Copies all ViVeTool Comments on the Database to a new Build
    @commentsgrp.command(name="copyall", description="Copies all existing ViVeTool Comments in the Database to a new Build")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def copyall(self, ctx, oldbuild: Option(int, "Enter the Build Number you want to copy from", required=True),
                      newbuild: Option(int, "Enter the Build Number you want to copy to", required=True)):
        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Check if oldbuild and newbuild are the same
        if oldbuild == newbuild:
            await send_embed_error(ctx, f"Incorrect Parameters. Both oldbuild and newbuild are {newbuild}",
                                   UseRespond=True)
            LogAndPrint(f"Tried to copy Data from Table {oldbuild} to {newbuild}", "warning")
            return

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
        DB_Cursor = DB_Connection.cursor()

        # Check if the Old Table Exists
        Exists_oldTable = await MySQL_CheckIfTableExists(oldbuild)
        if Exists_oldTable is False:
            await send_embed_error(ctx, f"No comment for Build {oldbuild} was ever created", UseRespond=True)
            LogAndPrint(f"Tried to copy a comment from Build {oldbuild} but the MySQL Table doesn't exist",
                        "warning")
            return

        # Check if the New Table Exists
        Exists_newTable = await MySQL_CheckIfTableExists(newbuild)
        if Exists_newTable is False:
            # Create a Table if it doesn't exist
            DB_Cursor.execute(
                f"CREATE TABLE {DB_Comments_Public}.`{newbuild}` AS (SELECT * FROM {DB_Comments_Public}.`{oldbuild}`);")
            DB_Cursor.close()

            await send_embed(ctx, f"Successfully created a Database Table for Build {newbuild}", discord.Color.green(),
                             UseRespond=True)
            LogAndPrint(f"Created MySQL Table {newbuild}", "info")

            await send_embed(ctx, f"Successfully copied all Comments from Build {oldbuild} to Build {newbuild}", discord.Color.green(),
                             UseRespond=True)
            LogAndPrint(f"Copied all Data from MySQL Table {oldbuild} to {newbuild}", "info")

            # Commit to the Database and close the connection
            DB_Connection.commit()
            DB_Connection.close()
            return
        else:
            # Add Values into the Table
            DB_Cursor.execute(
                f"INSERT INTO {DB_Comments_Public}.`{newbuild}` SELECT * FROM {DB_Comments_Public}.`{oldbuild}`;")
            DB_Cursor.close()

            await send_embed(ctx, f"Successfully copied all Comments from {oldbuild} to {newbuild}",
                             discord.Color.green(), UseRespond=True)
            LogAndPrint(f"Copied all Data from MySQL Table {oldbuild} to {newbuild}", "info")

            # Commit to the Database and close the connection
            DB_Connection.commit()
            DB_Connection.close()
            return

    # Bot /comments remove command. Removes a ViveTool GUI Comment from the Database
    @commentsgrp.command(name="remove", description="Removes an existing ViVeTool Comment in the Database")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def remove(self, ctx, build: Option(int, "Enter a Build Number", required=True),
                     featurename: Option(str, "Enter a ViVeTool Name that you want to remove the Comment from",
                                         required=True)):
        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
        DB_Cursor = DB_Connection.cursor()

        # Check if the MySQL Table exists
        Exists = await MySQL_CheckIfTableExists(build)
        if Exists is False:
            await send_embed_error(ctx, f"No comment for Build {build} exists", UseRespond=True)
            LogAndPrint("Tried to remove a comment that doesn't exist", "info")
            return

        # Check if the specified Feature exists
        if await MySQL_CheckIfRowExists(f"{DB_Comments_Public}.`{build}`", "FeatureName", f'"{featurename}"') is False:
            await send_embed_error(ctx, f"A comment for {featurename} in {build} doesn't exist.",
                                   "Use /comments add to add a comment", UseRespond=True)
            LogAndPrint("Tried to remove a Feature that doesn't exist in the DB", "warning")
            return

        # Delete the ViVeTool GUI Comment from the Database
        DB_Cursor.execute(f"DELETE FROM {DB_Comments_Public}.`{build}` WHERE FeatureName='{featurename}';")
        await send_embed(ctx, f"The comment for {featurename} in Build {build} was deleted successfully",
                         discord.Color.green(), UseRespond=True)
        LogAndPrint(f"A comment for {featurename} in {build} was deleted", "info")
        DB_Cursor.close()

        # Commit to the Database and close the connection
        DB_Connection.commit()
        DB_Connection.close()

    # Bot /comments ban command. Bans an IP Address from accessing the API Port
    @commentsgrp.command(name="ban", description="Bans a User from posting any more Comments to the Comments Database")
    @commands.has_role(DISCORD_ROLE_CommentsRW)
    async def ban(self, ctx, messageid: Option(int,
                                               'Enter the MessageID that you want to ban. It is found in the "New ViVeTool GUI Comment" Message',
                                               required=True)):
        # Defer Interaction because DB Interactions take a while
        await ctx.defer()

        # Check for root/sudo privileges
        if os.geteuid() != 0:
            await send_embed_error(ctx, "Insufficient Privileges to run /sbin/iptables", """The Python process running the Discord Bot **must** be running under root or with sudo in order to execute `/sbin/iptables` 

Ban action aborted.""", UseRespond=True)
            await ctx.send("<@762219738570555412>")
            LogAndPrint("No Permissions to run iptables for the /comments ban command", "critical")
            return

        # Establish DB Connection
        DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_API)
        DB_Cursor = DB_Connection.cursor()

        # Check if the MessageID exists
        DB_Cursor.execute(
            f"SELECT count(API_MessageID_List.ID) FROM {DB_Comments_API}.API_MessageID_List WHERE ID='{messageid}';")

        # Store Result in a Variable
        Result = DB_Cursor.fetchone()[0]

        if Result != 1:
            DB_Cursor.close()
            await send_embed_error(ctx, f"The Message ID {messageid} doesn't exist", UseRespond=True)
            LogAndPrint("Tried to ban a MessageID that doesn't exist", "info")
            return

        # Get the IP tied to the MessageID
        DB_Cursor.execute(f"SELECT IP FROM {DB_Comments_API}.API_MessageID_List WHERE ID='{messageid}';")

        # Try to Decrypt the IP
        try:
            IP = decrypt(DB_Cursor.fetchone()[0], os.getenv("ENC_PSWD"))
            DB_Cursor.close()
        except RuntimeError as exp:
            await send_embed_error(ctx, "An Exception occurred.", 'Contact the Owner for more information.',
                                   UseRespond=True)
            LogAndPrint(exp, "error")
            LogAndPrint('Decrypted IP returned None', "error")
            return

        # Ban the IP
        IPTables = subprocess.run(
            ['iptables', '-A', 'INPUT', '-p', 'tcp', '--dport', f'{API_PORT_ALT}', '-s', f'{IP}', '-j', 'DROP'])
        IPTables_direct = subprocess.run(
            ['iptables', '-A', 'INPUT', '-p', 'tcp', '--dport', f'{API_Port}', '-s', f'{IP}', '-j', 'DROP'])
        IPTables_save = subprocess.run('iptables-save | tee /etc/iptables/rules.v4', shell=True)

        if IPTables.returncode == 0 and IPTables_direct.returncode == 0 and IPTables_save.returncode == 0:
            await send_embed(ctx, f"User with MessageID {messageid} was successfully banned", discord.Color.green(),
                             UseRespond=True)
            LogAndPrint(f"Banned User with MessageID {messageid}", "info")
        else:
            await send_embed_error(ctx, "An Error occurred while trying to run the iptables command.",
                                   '''Contact the Owner for more information.
                                   
Exitcode != 0. See logs.''', UseRespond=True)
            LogAndPrint(f"Exit Code IPtables (API_PORT_ALT): {IPTables.returncode}", "error")
            LogAndPrint(f"Arguments IPTables (API_PORT_ALT): {IPTables.args}", "error")
            LogAndPrint(f"Exit Code IPtables (API_PORT): {IPTables_direct.returncode}", "error")
            LogAndPrint(f"Arguments IPTables (API_PORT): {IPTables_direct.args}", "error")
            LogAndPrint(f"Exit Code IPtables-save: {IPTables_save.returncode}", "error")
            LogAndPrint(f"Arguments IPTables-save: {IPTables_save.args}", "error")

        DB_Connection.close()


# Runs every Time an Extension is loaded
# Required by every Extension File and Cog
# Used for printing the Cog Status to the Console
# Syntax: bot.add_cog(CogName(bot))
def setup(bot):
    bot.add_cog(comments(bot))
    LogAndPrint("Comments Cog loaded", "info")


# Runs every Time an Extension is unloaded
# Used for printing the Cog Status to the Console
def teardown(bot):
    LogAndPrint("Comments Cog unloaded", "info")
