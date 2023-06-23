# Localization Infos

### With the addition of a Self-Hosted Weblate Instance, Translation of ViVeTool GUI and it's Feature Scanner is now done online, at https://weblate.rawrr.dev

### Translations can be added by either:

- Creating a Weblate Account
- Signing in with a GitHub / Google Account
- or by Annonymously suggesting a Translation


# Weblate How-To:

A basic Weblate How-To can be found for example on the Fedora Project Docs here: https://docs.fedoraproject.org/en-US/localization/weblate/

or more in detail at the official Weblate Documentations here: https://docs.weblate.org/en/latest/user/translating.html


# Localization Instructions

### While translating/suggesting on Weblate, please keep the following in mind:

-  Translation Keys, that look like these and end in something like **`.AutoSize`**, **`.Size`**, **`.Name`**, start with **`__DBG_`** or are just complete Gibberish are to be ignored.
   - An Exception to this is Translation Keys that End with **`.Text`** or **`.NullText`**

Usually these Translation Keys are automatically hidden and locked, although if you do see one that isn't locked create a GitHub Issue and I'll lock it

![Showing Examples of .AutoSize and .Size](https://github.com/PeterStrick/ViVeTool-GUI/assets/60312421/d4a9d4dd-a922-4a08-87ea-6adc9b453a7f) ![Showing a __DBG_ Example](https://github.com/PeterStrick/ViVeTool-GUI/assets/60312421/e74f51db-b432-430d-af31-8e0fdf82d3fc)
![image](https://github.com/PeterStrick/ViVeTool-GUI/assets/60312421/c15a5126-a413-40fa-9094-0c16bb63f67f)

- Spaces at the beginning or end of a Text, that are either 1, 2 or 6 Characters are to be included in your Translation as these Spaces are intentional
- Curly Brackets with Numbers in them like this `{4}` are placeholders for Numbers and other Text that get used later once the Program is running
  - For Example a Translation like this `Total Size: {0} MB`, would look like this while the Program is running: `Total Size: 25 MB` 
