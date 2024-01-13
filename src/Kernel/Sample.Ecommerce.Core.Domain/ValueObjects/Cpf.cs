using System.Text.RegularExpressions;

namespace Sample.Ecommerce.Core.Domain.ValueObjects;

public record Cpf
{
    public string Value { get; private set; }

    public Cpf(string cpf)
    {
        cpf = SanitizeCPF(cpf);
        if (!ValidateCPF(cpf))
        {
            throw new DomainException("CPF inválido");
        }

        Value = cpf;    
    }

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    private Cpf()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    {
        
    }

    private string SanitizeCPF(string cpf)
    {        
        return Regex.Replace(cpf, @"\D", "");
    }

    private bool ValidateCPF(string cpf)
    {
        if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
        {
            return false;
        }
        
        int total = 0;
        int[] weights = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 9; i++)
        {
            total += int.Parse(cpf[i].ToString()) * weights[i];
        }

        int remainder = total % 11;
        int digit_1 = remainder < 2 ? 0 : 11 - remainder;

        if (digit_1 != int.Parse(cpf[9].ToString()))
        {
            return false;
        }

        total = 0;
        weights = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 10; i++)
        {
            total += int.Parse(cpf[i].ToString()) * weights[i];
        }

        remainder = total % 11;
        int digit_2 = remainder < 2 ? 0 : 11 - remainder;

        return digit_2 == int.Parse(cpf[10].ToString());
    }
}
