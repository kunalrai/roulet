﻿using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(_Owin))]
public partial class _Owin
{
    public static Task CatchAll(IOwinContext ctx, Func<IOwinContext, Task> handler)
    {
        try
        {

            ctx.Response.Headers.Set("Cache-Control", "no-cache, no-store, must-revalidate");
            ctx.Response.Headers.Set("Pragma", "no-cache");
            ctx.Response.Headers.Set("Expires", "0");
            ctx.Response.Headers.Set("Access-Control-Allow-Origin", "*");
            ctx.Response.Headers.Set("Access-Control-Allow-Headers", "Accept, Content-Type, Authorization");
            ctx.Response.Headers.Set("Access-Control-Allow-Methods", "*");
            ctx.Response.Headers.Set("X-Content-Type-Options", "nosniff");

            var task = handler(ctx);

            return task;
        }
        catch (ObjectDisposedException)
        {
            return null;
        }
        catch (Exception e)
        {
            ctx.Response.StatusCode = 500;
            ctx.Response.ContentType = "text/plain";
            return ctx.Response.WriteAsync(e.Message);
        }
    }

    public void Configuration(Owin.IAppBuilder host)
    {
        host.MapAndLog("/_ws/connect", (app) =>
        {
            app.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, System.Web.WebSocks.Connect); });
        });

        UseAuth(host);
        UseGames(host);
        UseLogs(host);


    }

    static void UseAuth(IAppBuilder host)
    {

        host.MapAndLog("/auth/list", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.List); });
        });

        host.MapAndLog("/auth/filter", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.Filter); });
        });


        host.MapAndLog("/users/createledger", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.CreateLedger); });
        });

        host.MapAndLog("/auth/create", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.Create); });
        });

        host.MapAndLog("/auth/GetDistrict", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.GetDistrict); });
        });

        host.MapAndLog("/auth/update", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.Update); });
        });

        host.MapAndLog("/auth/delete", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.delete); });
        });

        host.MapAndLog("/auth/reset", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.reset); });
        });

        host.MapAndLog("/auth/register", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.Register); });
        });

        host.MapAndLog("/auth/bind", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, Auth.Users.Binded); });
        });

    }

    static void UseGames(IAppBuilder host)
    {
        host.MapAndLog("/games/start", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.Start); });
        });

        host.MapAndLog("/games/update", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.Update); });
        });

        host.MapAndLog("/games/find", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.Find); });
        });

        host.MapAndLog("/games/find-game", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.FindInfo); });
        });

        host.MapAndLog("/games/bet", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.Bet); });
        });

        host.MapAndLog("/games/end", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.End); });
        });

        host.MapAndLog("/games/list", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Games.List); });
        });

    }

    static void UseLogs(IAppBuilder host)
    {

        host.MapAndLog("/logs/log", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Logs.Log); });
        });

        host.MapAndLog("/logs/list", (handler) =>
        {
            handler.Run((IOwinContext ctx) => { return _Owin.CatchAll(ctx, crm.Logs.List); });
        });

    }

}

public static class _MapExtensions
{
    public static IAppBuilder MapAndLog(this IAppBuilder app, string pathMatch, Action<IAppBuilder> configuration)
    {
        return MapExtensions.Map(app, pathMatch, configuration);
    }
}