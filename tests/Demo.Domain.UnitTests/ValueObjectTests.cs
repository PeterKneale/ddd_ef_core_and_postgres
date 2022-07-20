using System;
using Demo.Core.Domain.Common;
using Demo.Core.Domain.Students;
using FluentAssertions;
using Xunit;

namespace Demo.Domain.UnitTests;

public class ValueObjectTests
{
    [Fact]
    public void Ensure_records_equal()
    {
        var name1 = new Name("first", "last");
        var name2 = new Name("first", "last");
        
        Assert.Equal(name1, name2);
    }
    
    [Fact]
    public void Student_create()
    {
        var name = new Name("first1", "last1");
        var studentId = StudentId.CreateInstance(Guid.NewGuid());
        var student = Student.CreateInstance(studentId, name);
        student.Id.Should().Be(studentId);
        student.Name.Should().Be(name);
        student.Name.FullName.Should().Be("first1 last1");
    }
    
    [Fact]
    public void Student_rename()
    {
        var studentId = StudentId.CreateInstance(Guid.NewGuid());
        var oldName = new Name("first1", "last1");
        var newName = new Name("first2", "last2");
        var student = Student.CreateInstance(studentId, oldName);
        student.ChangeName(newName);
        student.Name.Should().Be(newName);
    }
}