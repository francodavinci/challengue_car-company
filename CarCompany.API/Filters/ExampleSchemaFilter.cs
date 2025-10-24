using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using CarCompany.Application.DTOs;
using CarCompany.Domain.Enums;

namespace CarCompany.API.Filters
{
    /// <summary>
    /// Custom filter to add examples to Swagger schemas
    /// </summary>
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(SaleRequest))
            {
                schema.Example = new OpenApiObject
                {
                    ["distributionCenterID"] = new OpenApiString("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    ["carType"] = new OpenApiInteger(0)
                };
            }
            else if (context.Type == typeof(SaleResponse))
            {
                schema.Example = new OpenApiObject
                {
                    ["id"] = new OpenApiString("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    ["car"] = new OpenApiObject
                    {
                        ["model"] = new OpenApiInteger(0),
                        ["price"] = new OpenApiDouble(8000)
                    },
                    ["distributionCenterID"] = new OpenApiString("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    ["date"] = new OpenApiString("2024-01-15T10:30:00Z")
                };
            }
            else if (context.Type == typeof(TotalSalesResponse))
            {
                schema.Example = new OpenApiObject
                {
                    ["totalSales"] = new OpenApiDouble(25000),
                    ["totalUnits"] = new OpenApiInteger(3)
                };
            }
            else if (context.Type == typeof(SalesByDistributionCenterResponse))
            {
                schema.Example = new OpenApiObject
                {
                    ["totalAmount"] = new OpenApiDouble(15000),
                    ["totalUnits"] = new OpenApiInteger(2)
                };
            }
            else if (context.Type == typeof(SalesUnitsPercentageByCenterResponse))
            {
                schema.Example = new OpenApiObject
                {
                    ["centerPercentages"] = new OpenApiArray
                    {
                        new OpenApiObject
                        {
                            ["centerName"] = new OpenApiString("Centro Norte"),
                            ["modelPercentages"] = new OpenApiObject
                            {
                                ["0"] = new OpenApiObject
                                {
                                    ["units"] = new OpenApiInteger(2),
                                    ["percentage"] = new OpenApiDouble(66.67)
                                },
                                ["1"] = new OpenApiObject
                                {
                                    ["units"] = new OpenApiInteger(1),
                                    ["percentage"] = new OpenApiDouble(33.33)
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}
