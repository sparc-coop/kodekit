# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
1.	Run "Kodekit.Web" project.
2.  Database: uikit.database.windows.net
3.  Blob Storage: "uikit" blob container in the "kuviocreative" storage account
4.  UI-Kit resource group in Azure.

# Build and Test
1.  Published to https://ui-kit.azurewebsites.net/
2.  The idea is that a user uploads a css file and the file has an id that can be used to access it with https://ui-kit.azurewebsites.net/uikit/{GUID}
    a. The /uikit link redirects to the direct link of the file in blob storage. 