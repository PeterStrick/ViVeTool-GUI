# Imports
import discord, os, logging, inspect
import mysql.connector
from dotenv import load_dotenv
from typing import Literal, get_args

# Load .env
load_dotenv()

# Variables
VERSION = os.getenv("VERSION_BOT")
ICON_LOGO = os.getenv("ICON_LOGO")
ICON_CROSS = os.getenv("ICON_CROSS")
EMBED_FOOTER_TEXT = "ViVeTool GUI Comments Bot - {}".format(VERSION)
sql_config = {
    'user': os.getenv("MYSQL_DB_USER"),
    'password': os.getenv("MYSQL_DB_PSWD"),
    'host': 'localhost'
}
DB_Comments_Public = os.getenv("DB_COMMENTS_PUBLIC")


# Function that displays an Embed, with specified Title, Color and Description
async def send_embed(ctx, title, color, description="", UseRespond=False, Ephemeral=False):
    embed = discord.Embed(title=title, description=description, color=color)
    embed.set_footer(text=EMBED_FOOTER_TEXT, icon_url=ICON_LOGO)
    if UseRespond:
        await ctx.respond(embed=embed, ephemeral=Ephemeral)
    else:
        await ctx.send(embed=embed)
    LogAndPrint(f"Normal Embed sent. Title: {title}", "info")


# Function that displays an Error Embed, with specified Title and Description
async def send_embed_error(ctx, title, description="", UseRespond=False, UseFollowUpSend=False, Ephemeral=False):
    embed = discord.Embed(title=title, description=description, color=discord.Color.red())
    embed.set_author(name="An Error occurred", icon_url=ICON_CROSS)
    embed.set_footer(text=EMBED_FOOTER_TEXT, icon_url=ICON_LOGO)
    if UseRespond and UseFollowUpSend:
        await ctx.send_response(embed=embed, ephemeral=Ephemeral)
    elif UseRespond:
        await ctx.respond(embed=embed, ephemeral=Ephemeral)
    elif UseFollowUpSend:
        await ctx.send_followup(embed=embed, ephemeral=Ephemeral)
    else:
        await ctx.send_response(embed=embed, ephemeral=Ephemeral)
    LogAndPrint(f"Error Embed sent. Title: {title}. UseRespond: {UseRespond}, Ephemeral: {Ephemeral}", "error")


# Function that checks if a MySQL Table exists. Returns true if it exists, false if not
async def MySQL_CheckIfTableExists(tablename):
    DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
    DB_Cursor = DB_Connection.cursor()
    DB_Cursor.execute(f"SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{tablename}'")

    if DB_Cursor.fetchone()[0] == 1:
        DB_Cursor.close()
        DB_Connection.close()
        return True
    else:
        DB_Cursor.close()
        DB_Connection.close()
        return False


# Function that checks if a MySQL Row exists. Returns true if it exists, false if not
async def MySQL_CheckIfRowExists(tablename, valuename, value):
    DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_Public)
    DB_Cursor = DB_Connection.cursor()
    DB_Cursor.execute(f"SELECT EXISTS(SELECT 1 FROM {tablename} WHERE {valuename} = {value});")
    if DB_Cursor.fetchone()[0] != 0:
        DB_Cursor.close()
        DB_Connection.close()
        return True
    else:
        DB_Cursor.close()
        DB_Connection.close()
        return False


_LOG_MODES = Literal["debug", "info", "warning", "error", "critical", "fatal"]


def LogAndPrint(msg, LogMode: _LOG_MODES):
    assert LogMode in get_args(_LOG_MODES), f"'{LogMode}' is invalid - valid options are {get_args(_LOG_MODES)}"
    if LogMode == "debug":
        logging.debug(f"{inspect.stack()[1].filename} - {inspect.stack()[1].function} - {msg}")
        print(msg)
    elif LogMode == "info":
        logging.info(f"{inspect.stack()[1].filename} - {inspect.stack()[1].function} - {msg}")
        print(msg)
    elif LogMode == "warning":
        logging.warning(f"{inspect.stack()[1].filename} - {inspect.stack()[1].function} - {msg}")
        print(msg)
    elif LogMode == "error":
        logging.error(f"{inspect.stack()[1].filename} - {inspect.stack()[1].function} - {msg}")
        print(msg)
    elif LogMode == "critical":
        logging.critical(f"{inspect.stack()[1].filename} - {inspect.stack()[1].function} - {msg}")
        print(msg)
    elif LogMode == "fatal":
        logging.fatal(f"{inspect.stack()[1].filename} - {inspect.stack()[1].function} - {msg}")
        print(msg)
    else:
        raise AssertionError(f"'{LogMode}' is invalid - valid options are {get_args(_LOG_MODES)}")
