using System;

namespace Actio.Common.Commands
{
    public interface IAuthenticatedCommand
    {
        Guid UserId { get; set; }
    }
}