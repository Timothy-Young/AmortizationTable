using System;
using System.IO;
using System.Text;

namespace AmortizationTable
{
  class Program
  {
    public static double loan = 200000;   // $200,000

    static void Main(string[] args)
    {
      double rate1 = 0.03;    // 3%
      double rate2 = 0.025;   // 2.5%
      int term1 = 360;        // 30 years (30*12)
      int term2 = 240;        // 20 years (20*12)

      Console.WriteLine("Amortization Table for John Doe");
      Console.WriteLine(GenerateAmortizationTable(rate1, term1, 1));
      Console.WriteLine(GenerateAmortizationTable(rate2, term2, 2));
      Console.ReadKey();
    }

    private static string GenerateAmortizationTable(double rate, int term, int scenario)
    {
      StringBuilder sb = new StringBuilder();

      double balance = loan;
      int NumofPayments = term;
      double monthlyRate = (rate) / 12;
      double monthlyPayment = (monthlyRate / (1 - (Math.Pow((1 + monthlyRate), -(NumofPayments))))) * balance;
      double totalInterest = 0;

      sb.AppendLine();
      sb.AppendLine($"Scenario: {scenario}");
      sb.AppendLine();
      sb.AppendLine(string.Format("|{0,10}|{1,17}|{2,15}|{3,17}|{4,17}|", "Payment #", "Monthly Payment", "Interest", "Towards Principle", "Remaining Balance"));
      sb.AppendLine("----------------------------------------------------------------------------------");
      sb.AppendLine(string.Format("|{0,10}|{1,17}|{2,15}|{3,17}|{4,17}|", "0", "", "", "", balance));


      for (var i = 0; i < NumofPayments; i++)
      {
        double monthlyInterest = balance * monthlyRate;
        totalInterest += Math.Round(monthlyInterest, 2);
        double paymentTowardsPrinciple = monthlyPayment - monthlyInterest;
        balance -= paymentTowardsPrinciple;

        sb.AppendLine(string.Format("|{0,10}|{1,17}|{2,15}|{3,17}|{4,17}|", i + 1, Math.Round(monthlyPayment, 2), Math.Round(monthlyInterest, 2), Math.Round(paymentTowardsPrinciple, 2), Math.Round(balance, 2)));
      }

      sb.AppendLine("----------------------------------------------------------------------------------");
      sb.AppendLine();
      sb.AppendLine($"Total Interst Paid: {Math.Round(totalInterest, 2)}");
      sb.AppendLine();
      sb.AppendLine("----------------------------------------------------------------------------------");
      sb.AppendLine();

      string path = $"{Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Output\\Scenario_{scenario}.txt";
      File.WriteAllText(path, sb.ToString());
      return sb.ToString();
    }
  }
}
