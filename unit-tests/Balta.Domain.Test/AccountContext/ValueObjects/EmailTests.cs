using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Fact]
    public void ShouldLowerCaseEmail(){
        var input = "email@email.com";
        var emailInLowerCase = Email.EstaEmMinusculo(input);
        Assert.Equal(emailInLowerCase, input);
    }
    
    [Fact]
    public void ShouldTrimEmail(){
        var input = "email@email.com";
        var emailSemEspacos = Email.NaoTemEspacosEmBranco(input);
        Assert.Equal(emailSemEspacos, input);
    }
    
    [Fact]
    public void ShouldFailIfEmailIsNull() {
        string input = null;
        Assert.Throws<InvalidEmailException>(() => Email.EhNulo(input));
    }
    
    [Fact]
    public void ShouldFailIfEmailIsEmpty(){
        string input = string.Empty;
        Assert.Throws<InvalidEmailException>(() => Email.EstaVazio(input));
    }
    
    [Fact]
    public void ShouldFailIfEmailIsInvalid() {
        string input = "email.com";
        DataFake dataFake= new();
        Assert.Throws<InvalidEmailException>(() => Email.ShouldCreate(input, dataFake));
    }
    
    [Fact]
    public void ShouldPassIfEmailIsValid(){
        string input = "email@email.com";
        DataFake dataFake= new();
        Email email= Email.ShouldCreate(input, dataFake);
        Assert.IsType<Email>(email);
    }
    
    [Fact]
    public void ShouldHashEmailAddress(){
        string input = "email@email.com";
        DataFake dataFake= new();
        Email email = Email.ShouldCreate(input, dataFake);
        Assert.NotEmpty(email.Hash);
    }
    
    [Fact]
    public void ShouldExplicitConvertFromString() {
        string input = "email@email.com";
        DataFake dataFake= new();
        Email email = Email.ShouldCreate(input, dataFake);
        Assert.IsType<string>((string)email);
    }
    
    [Fact]
    public void ShouldExplicitConvertToString(){
        string input = "email@email.com";
        DataFake dataFake= new();
        Email email = Email.ShouldCreate(input, dataFake);
        Assert.IsType<string>(email.ToString());
    }
    
    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod() {
        string input = "email@email.com";
        DataFake dataFake= new();
        Email email = Email.ShouldCreate(input, dataFake);
        Assert.Equal(input, email.ToString());
    }
}

public class DataFake : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}