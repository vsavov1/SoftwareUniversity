using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_5_6_ATM_Withdrawal
{
    static class Program
    {
        static void Main(string[] args)
        {
            var context = new ATMDatebaseEntities();
            var account = context.CardAccounts.FirstOrDefault();
            var pin = "1234";
            decimal money = 200;

            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                bool isSucced = false;

                try
                {
                    if (account != null && (account.CardPIN == pin && account.CardCash >= money))
                    {
                        isSucced = true;
                    }

                    if (isSucced)
                    {
                        account.CardCash = account.CardCash - money;
                        context.TransactionsHistories.Add(
                            new TransactionsHistory()
                            {
                                Amount = money,
                                CardNumber = account.CardNumber,
                                Date = DateTime.Now
                            });

                        context.SaveChanges();
                        dbContextTransaction.Commit();

                        Console.WriteLine("Successfully withdrawaled money!");
                    }
                    else
                    {
                        throw new Exception("No money!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}