## Running Locally
After donwloading the repository, create a file under the Client/src/api folder with the name of local.settings.json containing the following configuration:
```
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
    }
}
```

1. Install Dependencies
Run npm install to get the development
environment ready.

```
./Client> npm i
```
2. Run Azurite


```
// Open a Command Line and run the following:
> azurite
```

3. Run the Taskify.API project

```
Open the VS Solution under the Server folder, and run the Taskify.API project.
```

4. Run the Angular Front End on a separate command line.
```
./Client> ng serve
```

By this point you should have the Server side running on the port `7155` and the Angular App running on port `4200`

5. You may now start the SWA CLI
```
./Client> npm run start:swa
```

This will make your app visible at `localhost:4280`


## Deploying to Azure

The web app requires an Azure Storage Account to be created. You can follow instructions here: [Create an Azure Storage Account](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal)

Once you have the storage account created, you can proceed to create the Azure Static Web App.

### Create Azure Static Web App
1. On your azure portal go to the top left burger menu, and select `Create a Resource`
2. Search for `Static Web App` using the search bar at the top.
3. Select the first option that appears, and click on `Create`

### Get connection string from storage account.
1. Go to your storage account, and on the left pane select `Access Keys`
2. Under `key1` copy the `Connection String` by clicking Show and then Copy to Clipboard.
3. Go to your Static Web App and and select `Configuration` under the Settings menu to the left.
4. Add a new Application Setting with name `AzureWebJobsStorage` and paste your connection string on the Value field.
5. Click on `Save` to save the changes.

### Copy Static Web App deployment Token to Github
1. Under your Static Web App Overview, find the `Manage deployment token` option in the top menu.
2. Copy the Deployment Token to clipboard.
3. Go to your GitHub repository page, and go to Settings > Secrets > Actions
4. Click on button with label `New repository secret`
5. Paste your token on the Secret field, and set the name of your secret as `SWA_DEPLOYMENT_TOKEN`
6. Save Changes

### Trigger Deployment
1. Go to the `Actions` tab in GitHub
2. Select the workflow `Azure Static Web Apps Deployment`
3. Click on `Run workflow`
4. Run the workflow for branch main.