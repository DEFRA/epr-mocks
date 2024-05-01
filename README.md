# EPR Mocks

## Overview

This repository contains projects used to mock various apis.


## Directory Structure 

This repository contains child projects as listed below.

### Mock Antivirus

Add a file local.settings.json (this will be gitignored).

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
    }
}
```


## Contributing to this project

Please read the [contribution guidelines](CONTRIBUTING.md) before submitting a pull request.

## Licence

[Licence information](LICENCE.md).
