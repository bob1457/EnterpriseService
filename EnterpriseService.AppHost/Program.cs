var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("messaging")
                                                        .WithManagementPlugin(); 

var inventoryService = builder.AddProject<Projects.EnterpriseService_Inventory_Api>("enterpriseservice-inventory-api")
                                                        .WithReference(messaging); ;

var notificationService = builder.AddProject<Projects.EnterpriseService_Notificatioin_Api>("enterpriseservice-notificatioin-api")
                                                        .WithReference(messaging); ;

var orderService = builder.AddProject<Projects.EnterpriseService_OrderService_Api>("enterpriseservice-orderservice-api")
                                                        .WithReference(messaging); ;

var shippingService = builder.AddProject<Projects.EnterpriseService_ShippingService_Api>("enterpriseservice-shippingservice-api")
                                                        .WithReference(messaging); ;

builder.Build().Run();
