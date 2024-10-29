using System.Text.RegularExpressions;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public partial record Email : ValueObject
{
    #region Constants

    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    #endregion

    #region Constructors

    private Email(string address, string hash, VerificationCode verificationCode)
    {
        Address = address;
        Hash = hash;
        VerificationCode = verificationCode;
    }

    #endregion

    #region Factories

    public static Email ShouldCreate(string address, IDateTimeProvider dateTimeProvider)
    {
        EhNulo(address);
        NaoTemEspacosEmBranco(address);
        EstaEmMinusculo(address);

        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException();

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProvider);

        return new Email(address, address.ToBase64(), verificationCode);
    }
    public static void  EstaVazio(string email){
        if(email == string.Empty)
            throw new InvalidEmailException();
    }
    public static string EstaEmMinusculo(string email){
        if(!email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidEmailException();
        return email;
    }

    public static void EhNulo(string email){
        if(email is null)
            throw new InvalidEmailException();
    }

    public static string NaoTemEspacosEmBranco(string email){
        if(email != email.Trim())
            throw new InvalidEmailException();
        return email;
    }

    #endregion

    #region Properties

    public string Address { get; }
    public string Hash { get; }
    public VerificationCode VerificationCode { get; }

    #endregion

    #region Methods

    public void ShouldVerify(string verificationCode) => VerificationCode.ShouldVerify(verificationCode);

    #endregion

    #region Operators

    public static implicit operator string(Email email)
        => email.ToString();

    #endregion

    #region Overrides

    public override string ToString() => Address;

    #endregion

    #region Other

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    #endregion
}