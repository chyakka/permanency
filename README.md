# Permanency

Permanency is a chat application made in C#, it uses WinForms for it's client and a command prompt for its server.

# Key Features

1. It uses TCP to communicate.
1. User information is stored on a MySql Database and uses the EntityFramework to communicate with it.
1. Uses BCrypt to secure user passwords.
1. Chat history is saved and stored in specific folders, dependant on the IP you're connected to.
1. Connected users are tracked and displayed on the clientside.

# To-do

1. Currently the database connection is established from the client-side instead of the server-side, this poses a lot of security risks, to temporarily negate this I have utilised an obfuscator for the client-side although this is not full proof.
1. Redesign for the client-side, I'm bad at UI and it looks ugly. Additionally the serverside could do with any form of UI.
1. VOIP is something I'm interested in implementing as this project was originally a joke when Discord was suffering from connection issues regularly, not a top priority issue at the moment.

# Bugs

1. Only known bug currently is if a server is terminated unexpectedly the client cannot disconnect/connect to another server without reloading the application.

# Usage & Support

To actually deploy this application for use you'll need to do the following:
1. Run the database.sql code on a MySQL database.
1. Modify the DatabaseContext.cs script on the client-side and insert your MySQL database details.
1. If you want to allow connections outside of your local network you will need to portforward, the port is 8888 by default.



Anyone is free to modify the application in any way, while I won't be offering specific support, any questions and queries create an issue and I'll respond to it when I can.
