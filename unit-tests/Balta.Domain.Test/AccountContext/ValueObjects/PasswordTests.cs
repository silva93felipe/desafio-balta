using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull() {
        string input = null;
        Assert.Throws<InvalidPasswordException>(() => { Password.ShouldCreate(input); });
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsEmpty(){
        string input = string.Empty;
        Assert.Throws<InvalidPasswordException>(() => { Password.ShouldCreate(input); });
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace() {
        string input = "    ";
        Assert.Throws<InvalidPasswordException>(() => { Password.ShouldCreate(input); });
    }
    
    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars(){
        string input = "1234567";
        Assert.Throws<InvalidPasswordException>(() => { Password.ShouldCreate(input); });
    }
    
    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars() {
        string input = "8888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888";
        Assert.Throws<InvalidPasswordException>(() => { Password.ShouldCreate(input); });
    }
    
    [Fact]
    public void ShouldHashPassword() {
        string input = "1234567891";
        Password password = Password.ShouldCreate(input);
        Assert.NotEmpty(password.Hash);
    }
    
    [Fact]
    public void ShouldVerifyPasswordHash() => Assert.Fail();
    
    [Fact]
    public void ShouldGenerateStrongPassword() => Assert.Fail();
    
    [Fact]
    public void ShouldImplicitConvertToString(){
        string input = "1234567891";
        Password password = Password.ShouldCreate(input);
        Assert.IsType<string>((string)password);
    }
    
    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod(){
        string input = "1234567891";
        Password password = Password.ShouldCreate(input);
        Assert.Equal(password.Hash, password.ToString());
    }
    
    [Fact]
    public void ShouldMarkPasswordAsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange() {
        string input = "1234567891";
        Password password = Password.ShouldCreate(input);
        Assert.True(password.MustChange);
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange(){
        string input = "1234567891";
        Password password = Password.ShouldCreate(input);
        password.MarkedAsMustChange();
        
    }
}