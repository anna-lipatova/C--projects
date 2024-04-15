﻿interface IValidator<in T>
{
    IEnumerable<ValidationError> Validate(T value);
}

class NonBlankStringValidator : IValidator<string>
{
	public IEnumerable<ValidationError> Validate(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new ValidationError($"\"{value} is empty or just whitespaces.\"");
		}
		return Array.Empty<ValidationError>();
	}
}

class RangeValidator<T> : IValidator<T> where T : IComparable<T>
{
	public T Minimum { get; init; }
	public T Maximum { get; init; }

	public IEnumerable<ValidationError> Validate(T value)
	{
		if (value.CompareTo(Minimum) < 0)
		{
			return new[] { new ValidationError($"\"{value} is less than minimum {Minimum}.\"") };
		}
		else if (value.CompareTo(Maximum) > 0)
		{
			return new[] { new ValidationError($"\"{value} is greater than maximum {Maximum}.\"") };
        }

		return Array.Empty<ValidationError>();
	}
}

class StringLengthValidator : IValidator<string>
{
	public RangeValidator<int> LengthValidator { get; init; }

	public StringLengthValidator(RangeValidator<int> lengthValidator)
	{
		LengthValidator	= lengthValidator;
	}

	public IEnumerable<ValidationError> Validate(string value)
	{
		var errors = LengthValidator.Validate(value.Length);

		List<ValidationError>? validationErrors = null;

		foreach(var error in errors)
		{
			if (validationErrors is null)
			{
				validationErrors = new List<ValidationError>();
			}

			validationErrors.Add(new ValidationError($"\"{value}\" length {error.Reason}"));
		}

		if (validationErrors is null)
		{
			return Array.Empty<ValidationError>();
		}
		else
		{
			return validationErrors;
		}
	}
}

class NotNullValidator: IValidator<object?>
{
	public IEnumerable<ValidationError> Validate(object? value)
	{
		if (value is null)
		{
			return new[] { new ValidationError($"\"{value}\" is null.") };
		}

		return Array.Empty<ValidationError>();
	}
}

/* TODO: any modifiers */ class ValidationError 
{
	public string Reason { get; init; }

	public ValidationError(string reason) {
		Reason = reason;
	}

	/* TODO: any additional members */
}

//

record class Order {
	public required string Id { get; set; }
	public int Amount { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Comment { get; set; }
}

record class SuperOrder : Order { }

//

class OrderValidator : Validator<Order> {
	// TODO:
	// ... Validate(Order value) ... {
	//	var allErrors = new List<ValidationError>();
	//	allErrors.AddRange(Validate(value.Amount, new RangeValidator<int> { Min = 1, Max = 10 }));
	//	allErrors.AddRange(Validate(value.Id, new NonBlankStringValidator(), new StringLengthValidator(new RangeValidator<int> { Min = 1, Max = 8 })));
	//	allErrors.AddRange(Validate(value.TotalPrice, new RangeValidator<decimal> { Min = 0.01M, Max = 999.99M }));
	//	allErrors.AddRange(Validate(value.Comment, new NotNullValidator()));
	//	return allErrors;
	// }
}

class AdvancedOrderValidator : Validator<Order> {
	// TODO:
	// ... Validate(Order value) ... {
	//	  Similar syntax as for OrderValidator, but more compact:
	//	  + without need to specify inferable types <int>, <decimal> ...
	//	  + without need for new ...
	// }
}

class Program {
	static void Main(string[] args) {
		Console.WriteLine("--- plain Validators ---");

		var nonBlankStringValidator = new NonBlankStringValidator();
		nonBlankStringValidator.Validate("   ").Print();
		nonBlankStringValidator.Validate("hello").Print();

		var rangeValidator = new RangeValidator<int> { Minimum = 1, Maximum = 6 };
		rangeValidator.Validate(7).Print();
		rangeValidator.Validate(1).Print();

		var stringLengthValidator = new StringLengthValidator(new RangeValidator<int> { Minimum = 5, Maximum = 6 });
		stringLengthValidator.Validate("Jack").Print();
		stringLengthValidator.Validate("hello-world").Print();
		stringLengthValidator.Validate("hello").Print();

		var notNullValidator = new NotNullValidator();
		object? obj = null;
		notNullValidator.Validate(obj).Print();
		string? s = null;
		notNullValidator.Validate(s).Print();
		Order? order = null;
		notNullValidator.Validate(order).Print();
		s = "hello";
		notNullValidator.Validate(s).Print();

		Console.WriteLine("--- AdvancedOrderValidator.Validate() ---");

		AdvancedOrderValidator advancedValidator = new AdvancedOrderValidator();

		var o1 = new Order { Id = "    ", Amount = 5 };
		advancedValidator.Validate(o1).Print();

		var o2 = new Order { Id = "AC405", Amount = 5 };
		advancedValidator.Validate(o2).Print();

		var o3 = new Order { Id = "AC405", Amount = 600 };
		advancedValidator.Validate(o3).Print();

		var o4 = new Order { Id = "", Amount = 600 };
		advancedValidator.Validate(o4).Print();

		var o5 = new Order { Id = "AC405-12345678", Amount = 5, TotalPrice = 42, Comment = "Best order ever" };
		advancedValidator.Validate(o5).Print();

		var o6 = new Order { Id = "AC405", Amount = 5, TotalPrice = 42, Comment = "Best order ever" };
		advancedValidator.Validate(o6).Print();

		Console.WriteLine("--- OrderValidator.Validate() ---");

		OrderValidator orderValidator = new OrderValidator();

		orderValidator.Validate(o1).Print();
		orderValidator.Validate(o2).Print();
		orderValidator.Validate(o3).Print();
		orderValidator.Validate(o4).Print();
		orderValidator.Validate(o5).Print();
		orderValidator.Validate(o6).Print();

		Console.WriteLine("--- ValidateSuperOrders() ---");

		var s1 = new SuperOrder { Id = "SO501", Amount = 5, TotalPrice = 42, Comment = "Super order 1" };
		var s2 = new SuperOrder { Id = "SO502", Amount = 700, TotalPrice = 41, Comment = "Super order 2" };
		var s3 = new SuperOrder { Id = "", Amount = 800, Comment = "Super order 2" };

		var orders = new List<SuperOrder> { s1, s2, s3 };
		ValidateSuperOrders(orders, orderValidator);

		Console.WriteLine("--- ValidateAll() ---");

		ValidateAll(orders, orderValidator);

		Console.WriteLine("--- ValidateAll<SuperOrder>() ---");

		ValidateAll<SuperOrder>(orders, orderValidator);
	}

	static void ValidateSuperOrders(IEnumerable<SuperOrder> orders, /* TODO: Validator for SuperOrder */ validator) {
		foreach (var o in orders) {
			validator.Validate(o).Print();
		}
	}

	static void ValidateAll<T>(IEnumerable<T> orders, /* TODO: Validator for T */ validator) {
		foreach (var o in orders) {
			validator.Validate(o).Print();
		}
	}
}
