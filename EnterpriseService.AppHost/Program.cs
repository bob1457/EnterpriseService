var builder = DistributedApplication.CreateBuilder(args);

var inventoryService = builder.AddProject<Projects.EnterpriseService_Inventory_Api>("enterpriseservice-inventory-api");

var notificationService = builder.AddProject<Projects.EnterpriseService_Notificatioin_Api>("enterpriseservice-notificatioin-api");

var orderService = builder.AddProject<Projects.EnterpriseService_OrderService_Api>("enterpriseservice-orderservice-api");

var shippingService = builder.AddProject<Projects.EnterpriseService_ShippingService_Api>("enterpriseservice-shippingservice-api");

builder.Build().Run();
