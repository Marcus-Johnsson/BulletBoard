# BulletBoard Frontend

SvelteKit frontend for the BulletBoard note-taking application.

## Prerequisites

Make sure your backend is running on `http://localhost:5000` (or update the `.env` file with your backend URL).

## Development

Start the development server:

```sh
npm run dev

# or open in browser automatically
npm run dev -- --open
```

The app will be available at http://localhost:5173

## Building

To create a production build:

```sh
npm run build
```

Preview the production build:

```sh
npm run preview
```

## Configuration

The API URL is configured in `.env`:
- `PUBLIC_API_URL` - Backend API URL (default: http://localhost:5000)

## Features

- ✍️ Create notes with title and content
- 📖 View all notes
- 🗑️ Delete notes
- ⚡ Real-time updates
- ❌ Error handling

## Project Structure

- `src/lib/api.ts` - API client for backend communication
- `src/routes/+page.svelte` - Main application page
- `.env` - Environment variables
