// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     This stores the information for a JQL where clause
/// </summary>
internal class Clause : IFinalClause
{
    private readonly IList<Tuple<Fields, bool?>> orderByList = new List<Tuple<Fields, bool?>>();
    private string finalClause;

    public Clause()
    {
    }

    public Clause(string finalClause)
    {
        this.finalClause = finalClause;
    }

    /// <summary>
    ///     The field to compare
    /// </summary>
    public Fields Field { get; set; }

    /// <summary>
    ///     The operator
    /// </summary>
    public Operators Operator { get; set; }

    /// <summary>
    ///     Value to compare with the operator
    /// </summary>
    public string Value { get; set; }

    public IFinalClause OrderBy(Fields field)
    {
        if (field == Fields.Labels)
        {
            throw new ArgumentException("Cannot order by something that can have multiple values, like label", nameof(field));
        }

        this.orderByList.Add(new Tuple<Fields, bool?>(field, null));
        return this;
    }

    public IFinalClause OrderByDescending(Fields field)
    {
        if (field == Fields.Labels)
        {
            throw new ArgumentException("Cannot order by something that can have multiple values, like label", nameof(field));
        }

        this.orderByList.Add(new Tuple<Fields, bool?>(field, true));
        return this;
    }

    public IFinalClause OrderByAscending(Fields field)
    {
        if (field == Fields.Labels)
        {
            throw new ArgumentException("Cannot order by something that can have multiple values, like label", nameof(field));
        }

        this.orderByList.Add(new Tuple<Fields, bool?>(field, false));
        return this;
    }

    /// <summary>
    ///     Change the operator to the negative version ( equals becomes not equals becomes equals)
    /// </summary>
    public void Negate()
    {
        Operator = Operator switch
        {
            Operators.Contains => Operators.DoesNotContain,
            Operators.DoesNotContain => Operators.Contains,
            Operators.EqualTo => Operators.NotEqualTo,
            Operators.NotEqualTo => Operators.EqualTo,
            Operators.In => Operators.NotIn,
            Operators.NotIn => Operators.In,
            Operators.GreaterThan => Operators.LessThan,
            Operators.GreaterThanEqualTo => Operators.LessThanEqualTo,
            Operators.LessThan => Operators.GreaterThan,
            Operators.LessThanEqualTo => Operators.GreaterThanEqualTo,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    /// <summary>
    ///     Add implicit casting to string
    /// </summary>
    /// <param name="clause">Clause</param>
    public static implicit operator string(Clause clause) => clause.ToString();

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(this.finalClause))
        {
            return this.finalClause;
        }

        var clauseBuilder = new StringBuilder();
        clauseBuilder.Append(Field.EnumValueOf()).Append(' ');
        clauseBuilder.Append(Operator.EnumValueOf()).Append(' ');
        clauseBuilder.Append(Value);
        if (this.orderByList.Any())
        {
            clauseBuilder.Append(" ORDER BY ");
            clauseBuilder.Append(string.Join(", ", this.orderByList.Select(orderBy =>
            {
                var order = orderBy.Item2.HasValue ? orderBy.Item2.Value ? " DESC" : " ASC" : "";
                return $"{orderBy.Item1.EnumValueOf()}{order}";
            })));
        }

        this.finalClause = clauseBuilder.ToString();

        return this.finalClause;
    }
}
