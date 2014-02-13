﻿using Template.Components.Alerts;
using Template.Data.Core;
using System;
using System.Web;
using System.Web.Mvc;

namespace Template.Components.Services
{
    public abstract class BaseService
    {
        private Boolean disposed;
        protected IUnitOfWork UnitOfWork
        {
            get;
            set;
        }

        public String CurrentLanguage
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["language"] as String;
            }
        }
        public String CurrentArea
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["area"] as String;
            }
        }
        public String CurrentController
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"] as String;
            }
        }
        public String CurrentAction
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["action"] as String;
            }
        }
        public String CurrentId
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["id"] as String;
            }
        }

        public String CurrentAccountId
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public IMessagesContainer AlertMessages
        {
            get;
            protected set;
        }

        public BaseService() : this(null)
        {
        }
        public BaseService(ModelStateDictionary modelState)
        {
            UnitOfWork = new UnitOfWork();
            AlertMessages = new MessagesContainer(modelState);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposed) return;
            UnitOfWork.Dispose();
            disposed = true;
        }
    }
}
