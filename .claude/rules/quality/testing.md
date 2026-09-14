---
paths:
  - "**/*Test*.cs"
  - "**/*.test.{ts,tsx}"
  - "**/*.spec.{ts,tsx}"
---
# Testing Rules

## General

- Add or update tests for changed behavior.
- Do not rely on untested refactoring for confidence.
- Keep tests focused on observable behavior and meaningful edge cases.
- Avoid brittle tests that duplicate implementation details.

## C#

- Use MSTest with Microsoft.NET.Test.Sdk.
- Use Moq when mocking is needed.
- Tests belong in the Tests project.
- Business and Data layers should be independently testable through interfaces and dependency injection.
- Prefer explicit types in tests. Do not use `var`.

## React and TypeScript

- Use Vitest and React Testing Library (this template does not use Jest).
- Test rendering behavior, user interactions, validation behavior, and pure functions as appropriate.
- Name test files using `{component}.{testType}.test.tsx`.
- Common test type values are `rendering`, `interaction`, and `unitTest`.
- If a function is extracted to improve component clarity, add or update unit tests for the function when behavior is non-trivial.
- Run `npm run test:coverage` when coverage validation is requested or affected.
