﻿using WebRestApi.Models;

namespace WebRestApi.Repository
{
    public abstract class CommentRepositoryBase : RepositoryBase<Comment, WebRestApiContext>
    {
        protected CommentRepositoryBase(WebRestApiContext context) : base(context) { }
    }
}
