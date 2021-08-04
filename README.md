# Import_Project
An application that receives and sends information using RabbitMQ and Docker while exporting using HTTP

User.cs:
To be edited and further developed
An object used to send the Post
Currently contains input variable which is loaded with the Json text to be sent
Should be further developed to apply directly with the needed import in the form desired

Program.cs: 
Establishes connection with ConnectionFactory
Using Docker it establishes a consumer on the RabbitMQ interface
It makes "DemoQueue" which is the channel for the message Receiver

MessageReceiver.cs:
Using RabbitMQ an email is read in with the HandleBasicDeliver method taking in all important information
The byte array is a parameter that contains the link to a file imported
The method readintext takes this byte array, converts it to a string and reads the data from the file

Next the method calls CreateJSON, a method that takes in the string that is the name of the file
The data is read in using StreamReader and taken line by line into an array
The array words contains the category names for the file
The while loop then goes until the end of the file reading in each line and matching it with the category names
This data is added to a JSON Object that uses the category name as the key and the data from the text as the value
Once this is done the JSON Object is written into a file named file.json

Finally the data is taken to PostJsonContent, a method that takes in the URL that is being sent to and a client object
Using StreamReader the file.json created earlier is read into a string
Next a User is created and the input value is assigned to the string containing the file data
The HTTPRequest then sends a message and creates a link to receive and fullfill the request
The data is then sent from the user and posted as requested by the HTTP Server
Once it is sent there is a success code posted


