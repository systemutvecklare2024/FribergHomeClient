using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FribergHomeClient.Validation
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; } = 8;

        public override bool IsValid(object? value)
        {
            var password = value as string;
            // not empty
            if (string.IsNullOrWhiteSpace(password)) return false;
            
            // Long enough
            if (password.Length < MinimumLength) return false;

            // Atleast one uppercase letter
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            
            // Atleast one lowcase letter
            if (!Regex.IsMatch(password, @"[a-z]")) return false;

            // Atleast one digit
            if (!Regex.IsMatch(password, @"[0-9]")) return false;

            // Atleast one symbol
            if (!Regex.IsMatch(password, @"[\W_]")) return false;

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} måste vara minst {MinimumLength} tecken och innehålla siffror, symboler och stora, samt små bokstäver";
        }
    }
}
