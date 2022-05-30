using System;
namespace OpenDictionary.Models;

public interface IEntity<T>
{
    T Id { get; }
}

public interface IEntity : IEntity<Guid> { }