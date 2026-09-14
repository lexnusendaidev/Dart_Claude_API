---
paths:
  - "**/*.{ts,tsx}"
  - "**/*.scss"
  - "**/.env*"
  - "**/package.json"
  - "**/vite.config.*"
  - "**/vitest.config.*"
  - "**/tsconfig*.json"
---
# React, TypeScript, and Front-End Rules

## React Architecture

- Use the repository's React template structure unless the existing app differs.
- Standard folders are `components`, `hooks`, `models`, `pages`, `scss`, and `utilities`.
- Pages may import from components, hooks, utilities, and models.
- Components may import from hooks, utilities, and models.
- Hooks may import from utilities and models.
- Utilities may import from models.
- Keep files co-located with closely related tests and helper functions.
- In a web app, barrel files should stop before the `src` root. Do not add a root `src/index.ts` for web apps.
- In a library, use a root `src/index.ts` barrel so consumers can import library exports.

## Folder Governance

- Do not change the root folder structure without approval. The standard folders are `components`, `hooks`, `models`, `pages`, `scss`, and `utilities` under `src`.
- Place new files inside the existing standard folder for their purpose. Do not create new top-level folders under `src`.
- If a new top-level folder genuinely seems necessary, stop and ask the user for approval before creating it.

## Imports and Aliases

- Prefer configured aliases for shared app imports.
- Common aliases include `@/*`, `@components/*`, `@hooks/*`, `@models/*`, `@pages/*`, `@scss/*`, and `@utilities/*`.
- Use simple relative imports such as `./MyLocalFile` for files private to the same folder and not exported through a barrel.
- Keep barrel files as the public interface for a folder.

## Components

- Use function components, not class components.
- Prefer const arrow function expressions.
- Return `ReactElement` when typing components directly unless the existing file uses a different project convention.
- Organize component files with imports first, then the component.
- Inside components, place hooks first and JSX return last.
- Keep components focused on rendering. Move reusable state rules, business decisions, and data manipulation into custom hooks or pure functions.
- Constants belong in the utilities constants file, even if currently used in one place.

## TypeScript Functions and Types

- Use arrow function expressions for application functions.
- Always specify parameter types and return types for exported functions.
- Avoid classes for React components and front-end services unless the team has explicitly accepted the exception.
- If overloads are required, order overload signatures from most specific to least specific.
- Avoid `any`.
- Before using `any`, attempt a primitive type, interface, documented external type, console-discovered object shape, union type, or colleague/tool-assisted troubleshooting.
- Union types are acceptable when concise and meaningful.
- If a union becomes long or repeated, create a named custom type or interface.
- Common interfaces belong in `models`.
- Private interfaces and types may stay in the folder where they are used.
- Interface files should be named `I{Name}.types.ts`.
- Custom type files should be named `{Name}.types.ts`.
- Interfaces that are not closely related should be in separate files.
- Shared repeated interface properties should be extracted into a base interface and extended.
- Use `Partial` or `Pick` when they reduce duplication without obscuring intent.

## Naming

- Use PascalCase for folders, files, React components, interfaces, and custom types.
- Use camelCase for variables and functions.
- Use SCREAMING_SNAKE_CASE for `.env` variables and constants.ts values.
- Test files should follow `{component}.{testType}.test.tsx`.
- Test type descriptors generally include `rendering`, `interaction`, or `unitTest`.

## React Router and Data Strategy

- Use React Router data APIs for route-level data, loaders, actions, nested routes, and layout outlets.
- Use loaders for initial page data and simple route-level data.
- Use actions for route-level mutations when appropriate.
- Use React Query for component-level data, frequently updating data, partial page updates, and realtime dashboards.
- Choose one realtime refresh model per page. Do not mix React Router `revalidate()` and React Query `invalidateQueries()` for the same page's data strategy.
- SignalR is a change notification mechanism, not a full data transport.
- SignalR events should not carry full data payloads.
- After SignalR change notifications, fetch current data from the API.
- Treat the API as the single source of truth.

## Forms

- Prefer React Hook Form for forms.
- Use react-bootstrap Form components where practical.
- Use `Form.Group`, `Form.Control`, and `Form.Control.Feedback` with React Hook Form when possible.
- Use helper text below fields instead of placeholders.
- Avoid reset buttons.
- Required fields should be labeled with `*`.
- Labels should be bold, including the required indicator.
- Spell out labels instead of using unexplained abbreviations.
- Tell users required formatting up front, such as `YYYY/MM/DD`.
- Small simple forms should use a single column with top labels, tight spacing, and left alignment.
- Larger complex forms should group logical sections, such as Employee Information or Manager Information.
- Field errors should outline the field in red and show error text immediately below the field.
- Error text should repeat the field label and state the requirement, such as `Phone Number is required`.
- Error messages should not include punctuation.
- Use group errors such as `Please complete all the required fields` or `Please correct the errors above`.

## UI and UX

- Reduce cognitive load through structure, transparency, clarity, and support.
- Use plain language at approximately a 6th to 8th grade reading level.
- Use positive language where practical.
- Ask one question per field.
- Communicate requirements before users begin.
- Use progress indicators for multi-step forms or long forms broken into steps.
- Use Bootstrap alert divs for text blocks and choose an appropriate alert type.
- Center alert blocks on the page and left-align their text.
- Match field length to expected input.
- Display validation errors after users move off a field.
- Do not rely on placeholders for critical instructions.
- Prefer the shared components in `src/components/buttons` over ad-hoc `<Button>` usage.
- Button labels should be consistent:
  - `Filter` filters table results outside data-grid auto-filtering (`FilterButton`).
  - `Search` searches a page (`SearchButton`).
  - `Submit` submits a form that navigates to a success page (`SubmitButton`).
  - `Save` performs incremental saves while the user stays on the page (`SaveButton`).
- Button placement should be on the right. Use `ActionButtonGroup` for consistent alignment.
- For multiple buttons, place the most-used button on the left within the button group.

## Tooling

- Use ESLint to enforce coding standards and architecture rules.
- Run linting with `npm run lint` when validating front-end changes.
- Do not change Prettier configuration without team agreement.
- Run formatting with `npm run format` when needed.
- Respect existing `.vscode` format-on-save settings.
- Environment files should include `.env.development`, `.env.uat`, and `.env.production` where the template expects them.
- When adding a new environment variable, update all environment files even if non-development values are placeholders.

## Test Coverage

- Use Vitest and React Testing Library for React and TypeScript tests (this template does not use Jest).
- Run test coverage with `npm run test:coverage`.
- Coverage output belongs under the root `coverage` folder and should remain ignored by git.
