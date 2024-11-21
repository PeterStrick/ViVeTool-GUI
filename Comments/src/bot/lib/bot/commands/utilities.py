# Imports
import discord, os
from discord.ext import commands
from discord.commands import SlashCommandGroup
from lib.bot.functions import send_embed, LogAndPrint
from dotenv import load_dotenv

# Load .env
load_dotenv()

# Variables
DISCORD_ROLE_Owner = int(os.getenv("DISCORD_ROLE_OWNER"))
VERSION = os.getenv("VERSION_BOT")
ICON_LOGO = os.getenv("ICON_LOGO")
EMBED_FOOTER_TEXT = "ViVeTool GUI Comments Bot - {}".format(VERSION)

# Rules Embed
embed_mods = discord.Embed(title="#comments Rules", color=discord.Color.gold())
embed_mods.add_field(name="1. Do not abuse the /comments add/edit/remove commands",
                     value="This will result in you losing access to <#992320061144571964>", inline=False)
embed_mods.add_field(name="2. Do not abuse the /comments ban command",
                     value="This will result in you being banned from the Discord Server.", inline=False)
embed_mods.set_footer(text=EMBED_FOOTER_TEXT, icon_url=ICON_LOGO)

embed_general = discord.Embed(title="General Rules", color=discord.Color.gold())
embed_general.add_field(name="1. Be polite and use common sense", value="This should be understandable",
                        inline=False)
embed_general.add_field(name="2. No Homophobic, Racist, Horrifying or Sexual content.",
                        value="This includes Racial/Homophobic Slurs, Gore, NSFW, etc", inline=False)
embed_general.add_field(name="3. Follow Discord's ToS.", value="[Link to the ToS](https://discord.com/terms)", inline=False)
embed_general.set_footer(text=EMBED_FOOTER_TEXT, icon_url=ICON_LOGO)


# Utilities Cog
class utilities(commands.Cog):
    def __init__(self, bot: discord.Bot):  # Make the Bot Variable accessible in Cogs
        self.bot = bot

    # Create "/comments" Slash Command Group
    utilitiesgrp = SlashCommandGroup("utilities", "Bot Utilities")

    # Bot /utilities ping command. Gets the Bots response time in ms
    @utilitiesgrp.command(name="ping", description="Returns the Bots response time in ms")
    async def ping(self, ctx: discord.ApplicationContext):
        await send_embed(ctx, "Pong!", discord.Color.green(), '{} ms'.format(round(self.bot.latency * 1000)), True)

    # Bot /utilities ping command. Gets the Bots response time in ms
    @commands.has_role(DISCORD_ROLE_Owner)
    @utilitiesgrp.command(name="sendrules", description="Sends the Discord Server Rules")
    async def sendrules(self, ctx: discord.ApplicationContext):
        await ctx.respond(embed=embed_mods)
        await ctx.respond(embed=embed_general)

    # Bot /utilities shutdown command. Shuts the Bot down. Useful while Developing the Bot
    @commands.has_role(DISCORD_ROLE_Owner)
    @utilitiesgrp.command(name="shutdown", description="Shuts down the Discord Bot")
    async def shutdown(self, ctx: discord.ApplicationContext):
        await send_embed(ctx, "Shutting down Bot", discord.Color.purple(), UseRespond=True, Ephemeral=True)
        await self.bot.close()

    # Bot /utilities reload command. Reloads all the Extension Files
    @commands.has_role(DISCORD_ROLE_Owner)
    @utilitiesgrp.command(name="reload", description="Reloads the Bots Extension Files")
    async def reload(self, ctx: discord.ApplicationContext):
        await send_embed(ctx, "Reloading Extension Files", discord.Color.purple(), UseRespond=True, Ephemeral=True)
        self.bot.reload_extension('lib.bot.commands.help')
        self.bot.reload_extension('lib.bot.commands.utilities')
        self.bot.reload_extension('lib.bot.commands.comments')

    # Bot /utilities resync command. Re-syncs the Slash Commands
    @commands.has_role(DISCORD_ROLE_Owner)
    @utilitiesgrp.command(name="resync", description="Resync the Slash Commands")
    async def resync(self, ctx: discord.ApplicationContext):
        await send_embed(ctx, "Re-syncing Commands", discord.Color.purple(), UseRespond=True, Ephemeral=True)
        print("Re-syncing Commands")
        await self.bot.sync_commands(method="bulk", delete_existing=True, force=True)


# Runs every Time an Extension is loaded
# Required by every Extension File and Cog
# Used for printing the Cog Status to the Console
# Syntax: bot.add_cog(CogName(bot))
def setup(bot):
    bot.add_cog(utilities(bot))
    LogAndPrint("Utilities Cog loaded", "info")


# Runs every Time an Extension is unloaded
# Used for printing the Cog Status to the Console
def teardown(bot):
    LogAndPrint("Utilities Cog unloaded", "info")
