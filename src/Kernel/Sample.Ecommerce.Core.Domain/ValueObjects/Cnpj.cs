using System.Text.RegularExpressions;

namespace Sample.Ecommerce.Core.Domain.ValueObjects;

public record Cnpj
{
    public string Value { get; init; }

    public Cnpj(string cnpj)
    {
        cnpj = SanitizeCNPJ(cnpj);
        if (!ValidateCNPJ(cnpj))
        {
            throw new ArgumentException("CNPJ inválido");
        }

        Value = cnpj;
    }

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    private Cnpj()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    {
    }


    private string SanitizeCNPJ(string cnpj)
    {        
        return Regex.Replace(cnpj, @"\D", "");
    }

    private bool ValidateCNPJ(string cnpj)
    {
        if (cnpj.Length != 14 || new string(cnpj[0], 14) == cnpj)
        {
            return false;
        }
     
        int total = 0;
        int[] weights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 12; i++)
        {
            total += int.Parse(cnpj[i].ToString()) * weights[i];
        }

        int remainder = total % 11;
        int digit_1 = remainder < 2 ? 0 : 11 - remainder;

        if (digit_1 != int.Parse(cnpj[12].ToString()))
        {
            return false;
        }

        total = 0;
        weights = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 13; i++)
        {
            total += int.Parse(cnpj[i].ToString()) * weights[i];
        }

        remainder = total % 11;
        int digit_2 = remainder < 2 ? 0 : 11 - remainder;

        return digit_2 == int.Parse(cnpj[13].ToString());
    }
}
