using System;
using System.Collections;
using System.Collections.Generic;

public class Enterprise : IEnterprise
{

    public int Count => throw new NotImplementedException();

    public void Add(Employee employee)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        throw new NotImplementedException();
    }

    public bool Change(Guid guid, Employee employee)
    {
        throw new NotImplementedException();
    }

    public bool Contains(Guid guid)
    {
        throw new NotImplementedException();
    }

    public bool Contains(Employee employee)
    {
        throw new NotImplementedException();
    }

    public bool Fire(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Employee GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public Position PositionByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public bool RaiseSalary(int months, int percent)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

