# Imports
import mysql.connector, os, requests, sys
from dotenv import load_dotenv
from flask import Flask, request
from flask_restful import Resource, Api
from waitress import serve
from lib.encryption import encrypt

# Load .env
load_dotenv()

# Variables
app = Flask(__name__)
api = Api(app)
Version = os.getenv("VERSION_API")
Webhook_URL = os.getenv("WEBHOOK_URL")
sql_config = {
    'user': os.getenv("MYSQL_DB_USER"),
    'password': os.getenv("MYSQL_DB_PSWD"),
    'host': 'localhost'
}
API_Port = os.getenv("API_PORT")
DB_Comments_API = os.getenv("DB_COMMENTS_API")
ICON_LOGO = os.getenv("ICON_LOGO")


# Debug
class REST_Debug(Resource):
    @staticmethod
    def get():
        return {"about": "Hello, World!"}

    @staticmethod
    def post():
        json = request.get_json()
        return {"you sent": json}, 201


class WebhookForward(Resource):
    @staticmethod
    def post():
        json = request.get_json()

        # Check if the JSON sent with the POST request includes the required JSON elements
        comment = "comment" in json
        build = "build" in json
        featurename = "featurename" in json

        if comment is False or build is False or featurename is False:
            # Either comment, build or featurename JSON elements are missing
            return {"message": "Invalid JSON. This API should only be used internally by ViVeTool GUI"}, 400
        else:
            try:
                # Update the MessageID
                DB_Connection = mysql.connector.connect(**sql_config, database=DB_Comments_API)
                DB_Cursor = DB_Connection.cursor()
                DB_Cursor.execute("UPDATE API_Current_MessageID SET ID=ID+1;")
                DB_Connection.commit()
                DB_Cursor.execute("SELECT ID FROM API_Current_MessageID;")
                MessageID = DB_Cursor.fetchone()

                # Prepare the Webhook JSON
                webhook_template_string = '''{{
  "content": "<@&992320358977908756>",
  "embeds": [
    {{
      "title": "New ViVeTool GUI Comment",
      "description": "Use /help for Bot Information",
      "color": 60415,
      "fields": [
        {{
          "name": "Build",
          "value": "{}",
          "inline": true
        }},
        {{
          "name": "Feature Name",
          "value": "{}",
          "inline": true
        }},
        {{
          "name": "Comment ID",
          "value": "{}",
          "inline": true
        }},
        {{
          "name": "Comment",
          "value": "{}"
        }}
      ],
      "footer": {{
        "text": "ViVeTool GUI Comments API - {}",
        "icon_url": "{}"
      }}
    }}
  ],
  "username": "ViVeTool GUI Comments",
  "avatar_url": "{}",
  "attachments": []
}}'''
                webhook = webhook_template_string.format(json["build"], json["featurename"], MessageID[0],
                                                         json["comment"], Version, ICON_LOGO, ICON_LOGO)

                # Send the Webhook
                header = {"Content-type": "application/json"}
                requests.post(Webhook_URL, data=webhook, headers=header)

                # Add the MessageID and IP to API_MessageID_List
                SQL_Insert_MessageID = 'INSERT INTO API_MessageID_List (ID, IP) VALUES ({}, "{}");'
                DB_Cursor.execute(SQL_Insert_MessageID.format(MessageID[0], encrypt(request.remote_addr, os.getenv("ENC_PSWD"))))
                DB_Connection.commit()

                # Close the DB Connection and return 201
                DB_Connection.close()
                return {"message": "Comment sent successfully."}, 201
            except BaseException as err:
                print(f"Unexpected {err=}, {type(err)=}", file=sys.stderr)
                return {"message": "An exception occurred.", "error": f"Unexpected {err=}, {type(err)=}"}, 500

    @staticmethod
    def get():
        return {"message": "This API Endpoint requires JSON POST requests."}, 400


class ViVeTool_DefaultResponse(Resource):
    @staticmethod
    def get():
        return {"message": "This API should only be used internally by ViVeTool GUI."}, 403

    @staticmethod
    def post():
        return {"message": "This API should only be used internally by ViVeTool GUI."}, 403


# Default Response
api.add_resource(ViVeTool_DefaultResponse, "/")
api.add_resource(WebhookForward, "/send_comment")

if __name__ == "__main__":
    if len(sys.argv) >= 2 and sys.argv[1] == "debug":
        api.add_resource(REST_Debug, "/debug")
        app.run(debug=True, host='0.0.0.0', port=API_Port)
    else:
        serve(app, host='0.0.0.0', port=API_Port, url_scheme='https')
