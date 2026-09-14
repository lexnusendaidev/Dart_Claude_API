# Nusenda Development Standards for AI Coding Agents

These instructions define the coding, architecture, testing, UI, and documentation standards an AI coding agent must follow when working in this repository.

## Operating Principles

- Follow the existing repository structure and naming conventions before introducing new patterns.
- Make the smallest safe change that satisfies the request.
- Do not refactor unrelated stable code. Apply the scout rule only to code already being changed.
- Prefer explicit, readable code over clever abstractions.
- Keep business logic out of DTOs, request objects, response objects, controllers, and React components.
- Do not invent architecture, package choices, validation rules, or workflows when the repository already defines them.
- If instructions conflict, prioritize the most specific path-scoped rule over broader rules.

## Clean Code Standards

- Code should read clearly and almost like prose.
- Use high-signal, low-noise code. Remove meaningless repetition, unnecessary comments, dead code, bloated methods, and unclear names.
- Prefer descriptive names over comments that explain unclear code.
- Use positive conditionals where practical.
- Avoid double negatives and deeply nested conditional logic.
- Extract complex conditionals into clearly named functions.
- Avoid magic numbers and unexplained string comparisons. Prefer constants, enums, or well-named variables.
- Do not use chained ternary expressions.
- Use declarative collection operations where they improve clarity.
- Methods and functions should do one thing well.
- Avoid flag arguments that cause one method to do multiple things.
- Keep variable lifetimes short. Declare variables close to where they are used.
- Avoid returning tuples or dictionaries to compensate for a method doing too much. Use a named model when multiple values are conceptually related.
- Do not catch exceptions that cannot be handled meaningfully at that level.
- Throw errors in lower-level methods when needed, and catch them at a level that can decide what to do.
- Use custom exceptions for expected business-rule failures. Let unexpected system exceptions bubble to the general handler.

## Documentation and Comments

- Comments should communicate intent, not restate obvious code.
- Avoid redundant comments, apology comments, defect-log comments, brace-tracker comments, zombie code, and divider comments.
- Use TODO comments sparingly and only for real follow-up work.
- Include documentation links when external documentation meaningfully explains a decision.
- For TypeScript functions, use JSDoc when the function's purpose, parameters, or return value are not immediately obvious.
- For C# public classes and methods, use XML summary comments consistent with the repository's documentation style.

## Constants and Strings

- Put reusable IDs, messages, configuration keys, query-string parameters, and condition values in constants.
- Prefer nested static constants or enums in C# when compile-time checking and IntelliSense are useful.
- Use language-specific JSON resource files for user-facing React text.
- App string keys should be descriptive and follow component + purpose with a numeric suffix when needed.
- Use SCREAMING_SNAKE_CASE for environment variable names and constants.ts values.

## Financial and Numeric Rules

- Never use floating-point types for money.
- In SQL, use DECIMAL(19,4) for decimal financial values. Do not use MONEY.
- In C#, use decimal for monetary calculations.
- Use datetime2 instead of datetime in SQL.
- Store strings as nvarchar.
