// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using Marketplace.Data;
using Prolix.Logic;
using Prolix.Domain;
using Marketplace.Domain.Security;

namespace Marketplace.Logic
{
    /// <summary>
    /// Ready-only repository bound to App Data Context
    /// </summary>
    /// <typeparam name="T">Model Type</typeparam>
    public abstract class RepositoryService<T> : RepositoryService<T, IDataContext>
        where T : class, IIdentifiable
    {
        #region Constructors

        public RepositoryService(IDataContext context) : base(context)
        {
        }

        public RepositoryService(IDataContext context, SecurityContext security) : this(context)
        {
            Security = security;
        }

        #endregion

        #region Properties

        public SecurityContext Security { get; }

        #endregion
    }
}
