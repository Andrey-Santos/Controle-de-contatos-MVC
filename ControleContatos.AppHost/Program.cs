var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ControleContatos>("controlecontatos");

builder.Build().Run();
