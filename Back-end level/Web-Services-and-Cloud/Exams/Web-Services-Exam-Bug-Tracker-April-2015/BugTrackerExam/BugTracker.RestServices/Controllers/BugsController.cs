using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace ShoppingCenter
{
     class Program
    {
        static ProductsCollection repo = new ProductsCollection();
        private const string INCORRECT_COMMAND = "Incorrect command";
        
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string command = Console.ReadLine();
                string commandResult = ProcessCommand(command);
                Console.WriteLine(commandResult);
            }
        }

        public static string ProcessCommand(string command)
        {
            int indexOfFirstSpace = command.IndexOf(' ');
            string method = command.Substring(0, indexOfFirstSpace);
            string parameterValues = command.Substring(indexOfFirstSpace + 1);
            string[] parameters =
                parameterValues.Split(';');
            switch (method)
            {
                case "AddProduct":
                    return repo.Add(parameters[0], parameters[2], parameters[1]);
                case "DeleteProducts":
                    if (parameters.Length == 1)
                    {
                        return repo.DeleteProductByProducer(parameters[0]);
                    }
                    else
                    {
                        return repo.DeleteProductByNameAndProducer(parameters[0], parameters[1]);
                    }
                case "FindProductsByName":
                    return repo.FindProductsByName(parameters[0]);
                case "FindProductsByPriceRange":
                    return repo.FindProductsByPriceRange(parameters[0],parameters[1]);
                case "FindProductsByProducer":
                    return repo.FindProductsByProducer(parameters[0]);
                default:
                    return INCORRECT_COMMAND;
            }
        }
    }

    class ProductsCollection
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        OrderedMultiDictionary<string, Product> productsByName = new OrderedMultiDictionary<string, Product>(true);
        OrderedMultiDictionary<string, Product> productsByProducer = new OrderedMultiDictionary<string, Product>(true);
        OrderedMultiDictionary<decimal, Product> productsByPriceRange = new OrderedMultiDictionary<decimal, Product>(true);
        OrderedMultiDictionary<string, Product> productsByNameAndProducer = new OrderedMultiDictionary<string, Product>(true);

        public string Add(string name, string producer, string price)
        {
            Product newProduct = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                Producer = producer
            };

            productsByName.Add(newProduct.Name, newProduct);
            productsByProducer.Add(newProduct.Producer, newProduct);
            productsByPriceRange.Add(newProduct.Price, newProduct);
            productsByNameAndProducer.Add(newProduct.Name + "!_!" + newProduct.Producer, newProduct);

            return PRODUCT_ADDED;
        }

        private string PrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var builder = new StringBuilder();
                foreach (var product in products)
                {
                    builder.AppendLine(product.ToString());
                }
                string formattedProducts = builder.ToString().TrimEnd();
                return formattedProducts;
            }

            return NO_PRODUCTS_FOUND;
        }

      public string FindProductsByName(string name)
        {
            var products = productsByName[name]
                ;
            return SortAndPrintProducts(products);
        }

        public string FindProductsByProducer(string producer)
        {
            var products = productsByProducer[producer]
                ;
            return SortAndPrintProducts(products);
        }

        public string FindProductsByPriceRange(string start, string end)
        {
            decimal rangeStart = decimal.Parse(start);
            decimal rangeEnd = decimal.Parse(end);

            var products = productsByPriceRange.Range(rangeStart, true, rangeEnd, true).Values
                 ;
            return SortAndPrintProducts(products);
        }

        private string SortAndPrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var builder = new StringBuilder();
                foreach (var product in products)
                {
                    builder.AppendLine(product.ToString());
                }

                builder.Length -= Environment.NewLine.Length;

                string formattedProducts = builder.ToString();
                return formattedProducts;
            }

            return NO_PRODUCTS_FOUND;
        }


        public string DeleteProductByNameAndProducer(string name, string producer)
        {
            string nameAndProducerKey = name + "!_!" + producer;
            var productsToBeRemoved = productsByNameAndProducer[nameAndProducerKey];
            if (productsToBeRemoved.Any())
            {
                int countOfRemovedProducts = productsToBeRemoved.Count;
                foreach (var product in productsToBeRemoved)
                {
                    productsByName.Remove(product.Name, product);
                    productsByProducer.Remove(product.Producer, product);
                    productsByPriceRange.Remove(product.Price, product);
                }
                productsByNameAndProducer.Remove(nameAndProducerKey);
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;

        }

        public string DeleteProductByProducer(string producer)
        {
            var productsToBeRemoved = productsByProducer[producer];
            if (productsToBeRemoved.Any())
            {
                foreach (var product in productsToBeRemoved)
                {
                    productsByName.Remove(product.Name, product);
                    string nameAndProducerKey = product.Name + "!_!" + producer;
                    productsByNameAndProducer.Remove(nameAndProducerKey, product);
                    productsByPriceRange.Remove(product.Price, product);
                }
                int countOfRemovedProducts = productsByProducer[producer].Count;
                productsByProducer.Remove(producer);
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;

        }
    }

    class Product : IComparable<Product>
    {
        public string Name { get; set; }

        public string Producer { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            int resultOfCompare = this.Name.CompareTo(other.Name);
            if (resultOfCompare == 0)
            {
                resultOfCompare = this.Producer.CompareTo(other.Producer);
            }

            if (resultOfCompare == 0)
            {
                resultOfCompare = this.Price.CompareTo(other.Price);
            }

            return resultOfCompare;
        }

        public override string ToString()
        {
            string toString =
                "{" +
                this.Name + ";" +
                this.Producer + ";" +
                this.Price.ToString("0.00") +
                "}";
            return toString;
        }

    }
}