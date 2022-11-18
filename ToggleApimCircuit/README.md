# Help

```bash
az functionapp plan create --resource-group mohamed-apim1 --name myPremiumPlan --location eastus --number-of-workers 1 --sku EP1 --is-linux
az functionapp create --name apimcircuitbreaker --storage-account apimaux1 --resource-group mohamed-apim1g --plan myPremiumPlan --deployment-container-image-name apimcr1.azurecr.io/toggleapimcircuit:latest
```
