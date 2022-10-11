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

1. Create an Static Web App
2. 