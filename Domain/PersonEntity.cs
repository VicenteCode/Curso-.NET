using System.Text.RegularExpressions;

namespace Domain
{
    public class PersonEntity
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public PersonEntity(string code, string firstName, string lastName, string email, string phoneNumber)
        {
            ValidateCode(code);
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            Id = Guid.NewGuid();
            Code = code.Trim().ToUpper();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLower();
            PhoneNumber = phoneNumber.Trim();
        }

        public void UpdatePersonalInfo(string firstName, string lastName, string email, string phoneNumber)
        {
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLower();
            PhoneNumber = phoneNumber.Trim();
        }

        private void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código no puede estar vacío.", nameof(code));

            if (code.Trim().Length < 3)
                throw new ArgumentException("El código debe tener al menos 3 caracteres.", nameof(code));

            if (code.Trim().Length > 20)
                throw new ArgumentException("El código no puede exceder los 20 caracteres.", nameof(code));
        }

        private void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(firstName));

            if (firstName.Length < 2)
                throw new ArgumentException("El nombre debe tener al menos dos caracteres.", nameof(firstName));

            if (firstName.Length > 50)
                throw new ArgumentException("El nombre no debe exceder 50 caracteres.", nameof(firstName));
        }

        private void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("El apellido no puede estar vacío.", nameof(lastName));

            if (lastName.Length < 2)
                throw new ArgumentException("El apellido debe tener al menos 2 caracteres.", nameof(lastName));

            if (lastName.Length > 50)
                throw new ArgumentException("El apellido no debe exceder 50 caracteres.", nameof(lastName));
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico no puede estar vacío.", nameof(email));

            if (email.Length > 100)
                throw new ArgumentException("El correo electrónico no puede exceder los 100 caracteres.", nameof(email));

            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
                throw new ArgumentException("El formato del correo electrónico es inválido.", nameof(email));
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("El número de teléfono no puede estar vacío.", nameof(phoneNumber));

            if (phoneNumber.Trim().Length < 7)
                throw new ArgumentException("El número de teléfono debe tener al menos 7 caracteres.", nameof(phoneNumber));

            if (phoneNumber.Trim().Length > 15)
                throw new ArgumentException("El número de teléfono no puede exceder 15 caracteres.", nameof(phoneNumber));
        }
    }
}
