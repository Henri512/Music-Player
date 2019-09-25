using System;
using System.Linq.Expressions;

namespace MusicPlayer.Utilities.Helpers
{
    public interface IExpressionHelper
    {
        Expression<Func<T, bool>> BuildPredicate<T>(
            string propertyName, string comparison, string value);
    }
}