# How it works behind the scenes

The ViVeTool GUI comments System works in the following order:

1. User richt-clicks a Feature in ViVeTool GUI and selects "Add comment"
2. The "Add comment" Form opens where the User is able to write a comment
3. Upon pressing on "Send comment" a privacy policy will be shown that displays the information that will be sent to rawrr.cf to be stored in a Database
4. The data wil then be sent to an API as JSON which will validate it.
5. Once Validated a MessageID will be created which will be linked to the senders IP.
6. After that, The Build, Feature Name, Comment and MessageID will be forwarded to a Discord Server
7. There Moderators or Volunteers can Add / Edit / Remove Comments which will then be displayed in ViVeTool GUI for the specified Feature and Build

<!---
<img width="1171" alt="Zeichnung" src="https://user-images.githubusercontent.com/60312421/178966394-fa7e1b48-a3e4-45ff-9614-7e234b09ea09.png">
--->

# Why do you need the IP?
The Comment senders IP will be linked to a MessageID, in order to prevent abuse and hateful comments.

Only the Discord Bot internally sees the IP for the `!ban` command, which will inturn run an IPTables command which then blocks the IP from accessing the API Port.

Moderators are only able to see the MessageID and never the IP it self.

The Table holding the linked MessageIDs and IPs will be cleared every day using a cronjob that runs the following MySQL command:
```mysql
TRUNCATE ViVeTool_GUI_API.API_MessageID_List;
```

# What Data will be sent to the API / forwarded to the Discord Server?
The following JSON will be sent from ViVeTool GUI to the API: (This is an example)
```json
{
 "comment":"Test Comment",
 "build":"12345",
 "featurename":"Test1"
}
```

The API will then create a MessageID (which is just a integer Number that rises by 1) and link that with the sender IP in the DB

After that, the following JSON would be sent from the API to a Discord WebHook:
```json
{
  "content": null,
  "embeds": [
    {
      "title": "New ViVeTool GUI Comment",
      "description": "Use !help for Bot Information.",
      "color": 60415,
      "fields": [
        {
          "name": "Build",
          "value": "12345",
          "inline": true
        },
        {
          "name": "Feature Name",
          "value": "Test1",
          "inline": true
        },
        {
          "name": "Comment ID",
          "value": "12",
          "inline": true
        },
        {
          "name": "Comment",
          "value": "Test Comment"
        }
      ],
      "footer": {
        "text": "ViVeTool GUI Comments API - v1.1",
        "icon_url": "https://cdn.discordapp.com/attachments/992324600182620230/992325669470085190/output-onlinepngtools2.png"
      }
    }
  ],
  "username": "ViVeTool GUI Comments",
  "avatar_url": "https://cdn.discordapp.com/attachments/992324600182620230/992325669470085190/output-onlinepngtools2.png",
  "attachments": []
}

```

# Other
Source Code for the API and the Discord Bot will be available [in the Comments folder](https://github.com/PeterStrick/ViVeTool-GUI/tree/master/Comments)

### I am also open for suggestions or alternatives for the API/Discord System
