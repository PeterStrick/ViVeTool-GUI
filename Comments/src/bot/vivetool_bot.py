# Imports
import discord, os, logging, traceback
from dotenv import load_dotenv
from discord.ext import commands
from lib.bot.functions import send_embed_error, LogAndPrint

# Load .env
load_dotenv()

# Variables
DISCORD_TOKEN = os.getenv("DISCORD_TOKEN")
LOG_FILE = os.getenv("LOG_FILE")
intents = discord.Intents.default()
bot = discord.Bot(intents=intents)

# Set Up Logging:
logging.basicConfig(filename=LOG_FILE, level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s',
                    datefmt='%d.%m.%Y %H:%M')


# Print Message in Console when the Bot logs in
@bot.event
async def on_ready():
    LogAndPrint(f'Logged in as {bot.user}', "info")


# Bot App Command Error Handler
@bot.event
async def on_application_command_error(ctx, error):
    if isinstance(error, commands.MissingRequiredArgument):  # Missing required Argument
        await send_embed_error(ctx, "Missing Required Argument",
                               f"This command is missing the required {error.param} argument.", UseRespond=True,
                               Ephemeral=True)
    elif isinstance(error, commands.TooManyArguments):  # Too many Arguments
        await send_embed_error(ctx, "Too many Arguments", "This command does not support this amount of Arguments",
                               UseRespond=True, Ephemeral=True)
    elif isinstance(error,
                    commands.BadArgument or commands.BadUnionArgument or commands.BadLiteralArgument or commands.ArgumentParsingError):  # Argument Error
        await send_embed_error(ctx, "An Error occurred", "An Error occurred while parsing your command",
                               UseRespond=True, Ephemeral=True)
        LogAndPrint(''.join(traceback.format_exception(type(error), error, error.__traceback__)), "error")
    elif isinstance(error, commands.CommandNotFound):  # Command not found
        await send_embed_error(ctx, "Command not found", "This command was not found", UseRespond=True, Ephemeral=True)
    elif isinstance(error, commands.MissingPermissions or commands.MissingRole):  # Missing Permissions
        await send_embed_error(ctx, "Missing Permissions",
                               "You do not have the required Permissions to use this command", UseRespond=True,
                               Ephemeral=True)
    elif isinstance(error, commands.BotMissingPermissions or commands.BotMissingRole):  # Bot Missing Permissions
        await send_embed_error(ctx, 'An Error occurred', '''Your command could not be processed, because the Discord Bot is missing Permissions.

Please contact the Owner or an Admin''', UseRespond=True, Ephemeral=False)
    elif isinstance(error, commands.DisabledCommand):  # Command is disabled
        await send_embed_error(ctx, "Command is disabled", "This command is currently disabled", UseRespond=True,
                               Ephemeral=True)
    elif isinstance(error, commands.CommandOnCooldown):  # Command is on cooldown
        await send_embed_error(ctx, "Command on Cooldown", f'''This command is currently on Cooldown.

Please retry after {int(error.retry_after)} seconds.''', UseRespond=True, Ephemeral=True)
    else:  # Raise an Error if it is not caught
        LogAndPrint(''.join(traceback.format_exception(type(error), error, error.__traceback__)), "error")
        raise error


# Load Extensions
bot.load_extension('lib.bot.commands.help')
bot.load_extension('lib.bot.commands.comments')
bot.load_extension('lib.bot.commands.utilities')

# Run Bot
bot.run(DISCORD_TOKEN)
