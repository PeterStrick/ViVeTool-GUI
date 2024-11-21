# Imports
import discord, os
from discord.ext import commands
from dotenv import load_dotenv
from lib.bot.functions import LogAndPrint

# Load .env
load_dotenv()

# Variables
VERSION = os.getenv("VERSION_BOT")
ICON_LOGO = os.getenv("ICON_LOGO")
EMBED_FOOTER_TEXT = "ViVeTool GUI Comments Bot - {}".format(VERSION)
DISCORD_ROLE_Owner = int(os.getenv("DISCORD_ROLE_OWNER"))

# The Help Embed
Embed_Help = discord.Embed(title="Bot Help", color=discord.Color.green())
Embed_Help.add_field(name="/help", value="Shows this message", inline=False)
Embed_Help.add_field(name="/utilities ping", value="Returns the Bots response time in ms", inline=False)
Embed_Help.add_field(name='/comment get', value='''Gets a Table of every Comment for that Build only works in <#992320061144571964>)
Example: `/comment get`''', inline=False)
Embed_Help.add_field(name='/comment add <FeatureName> "<Comment>"', value='''Adds a Comment for the specified Feature (only works in <#992320061144571964>)
Example: `/comment add Feature1 "This appears as a comment"`''', inline=False)
Embed_Help.add_field(name='/comment edit <FeatureName> "<Comment>"', value='''Edits a comment from the specified Feature (only works in <#992320061144571964>)
Example: `/comment edit Feature1 "Edited comment"`''', inline=False)
Embed_Help.add_field(name="/comment remove <FeatureName>", value='''Removes a comment from the specified Feature (only works in <#992320061144571964>)
Example: `/comment remove Feature1`''', inline=False)
Embed_Help.add_field(name="/comment ban <MessageID>", value='''Permanently bans a User from posting comments and accessing the API (only works in <#992320061144571964>)
Example: `/comment ban 69`''', inline=False)
Embed_Help.set_footer(text=EMBED_FOOTER_TEXT, icon_url=ICON_LOGO)

# Additional help Embed only accessible to the Owner
Embed_Help_Owner = discord.Embed(title="Additional Bot Help", color=discord.Color.purple())
Embed_Help_Owner.add_field(name="/utilities sendrules", value="Sends the Discord Server Rules in Embed Format",
                           inline=False)
Embed_Help_Owner.add_field(name="/utilities reload", value="Reloads the Bots Extension Files", inline=False)
Embed_Help_Owner.add_field(name="/utilities shutdown", value="Shuts the Discord Bot down", inline=False)
Embed_Help_Owner.set_footer(text=EMBED_FOOTER_TEXT, icon_url=ICON_LOGO)


# Help Cog (Without Slash Command Group
class helpCog(commands.Cog):
    def __init__(self, bot: discord.Bot):  # Make the Bot Variable accessible in Cogs
        self.bot = bot

    # Bot /help command. Displays the Bots help
    @commands.slash_command(name="help", description="Shows the Bot Command Help")
    async def help(self, ctx: discord.ApplicationContext):
        await ctx.respond(embed=Embed_Help)

    # Bot /ownerhelp command. Displays additional Bots help
    @commands.has_role(DISCORD_ROLE_Owner)
    @commands.slash_command(name="ownerhelp", description="Shows additional Bot Command Help")
    async def ownerhelp(self, ctx: discord.ApplicationContext):
        await ctx.respond(embed=Embed_Help_Owner, ephemeral=True)


# Runs every Time an Extension is loaded
# Required by every Extension File and Cog
# Used for printing the Cog Status to the Console
# Syntax: bot.add_cog(CogName(bot))
def setup(bot):
    bot.add_cog(helpCog(bot))
    LogAndPrint("Help Cog loaded", "info")


# Runs every Time an Extension is unloaded
# Used for printing the Cog Status to the Console
def teardown(bot):
    LogAndPrint("Help Cog unloaded", "info")
