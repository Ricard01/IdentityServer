using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identity.Infrastructure.Constants
{

    public static class Permissions
    {       
        public static List<string?> GeneratePermissionsForModule(Type type)
        {
            return type.GetFields().Select(x => x.GetValue(null)?.ToString()).ToList();
        }

        public static List<string?> GenerateAllPermissions()
        {
            var permisos = typeof(Catalogs).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList();

            permisos.AddRange(typeof(Distribution).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList());

            permisos.AddRange(typeof(Inventory).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList());

            permisos.AddRange(typeof(Distribution).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList());

            permisos.AddRange(typeof(User).GetFields().Select(x => x.GetValue(null)?.ToString()).ToList());

            return permisos;
        }

        public static class Catalogs
        {
            public const string Banks = "permission.Catalogs.Banks";
            public const string Customers = "permission.Catalogs.Customers";
            public const string Employees = "permission.Catalogs.Employees";
            public const string Products = "permission.Catalogs.Products";
            public const string Suppliers = "permission.Catalogs.Suppliers";
            public const string Zones = "permission.Catalogs.Zones";
            public const string Packages = "permission.Catalogs.Packages";
            public const string Brands = "permission.Catalogs.Brands";
        };

        public static class Distribution
        {
            public const string Cash = "permission.Distribution.Cash";
            public const string Deliveries = "permission.Distribution.Deliveries"; // Include Permission to DeliveryItems
        }

        public static class Inventory
        {
            public const string Movements = "permission.Inventory.Movements";
            public const string Stock = "permission.Inventory.Stock";
            public const string Warehouses = "permission.Inventory.Warehouses";
            public const string Manufacturer = "permission.Inventory.Manufacturer";
        }

        public static class Order
        {
            public const string Orders = "permission.Order.Orders"; // Include Permission to OrdersItems
            public const string Purchases = "permission.Order.Purchases"; // Include Permission to PurchasesItems
        };

        public static class User
        {
            public const string Users = "permission.User.Users";
            public const string Roles = "permission.User.Roles";
        }

    }

    public enum PermissionOperator
    {
        And = 1,
        Or = 2
    }
}
