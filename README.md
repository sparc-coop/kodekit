# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
1.	Currently only the "UI-Kit" projects works. "UI-Kit.Web" is the WebAssembly version that hasn't been hooked up yet.
2.  Database: uikit.database.windows.net
3.  Blob Storage: "uikit" blob container in the "kuviocreative" storage account
4.  UI-Kit resource group in Azure.

# Build and Test
1.  Published to https://ui-kit.azurewebsites.net/
2.  The idea is that a user uploads a css file and the file has an id that can be used to access it with https://ui-kit.azurewebsites.net/uikit/{GUID}
    a. The /uikit link redirects to the direct link of the file in blob storage. 

# Contribute
1. Needs user authentication and the user flow process.
2. Ability to import other file types or add content manually.
3. Add frontend styling when designs are complete.
