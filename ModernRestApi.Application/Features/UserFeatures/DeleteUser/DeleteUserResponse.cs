﻿namespace ModernRestApi.Application.Features.UserFeatures.DeleteUser
{
    public sealed record DeleteUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
    }
}
