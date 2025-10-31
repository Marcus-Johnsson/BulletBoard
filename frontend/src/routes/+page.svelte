<script lang="ts">
	import { onMount } from 'svelte';
	import { api, type Note } from '$lib/api';

	let notes: Note[] = [];
	let title = '';
	let description = '';
	let loading = false;
	let error = '';

	onMount(async () => {
		await loadNotes();
	});

	async function loadNotes() {
		try {
			loading = true;
			error = '';
			notes = await api.getNotes();
		} catch (e) {
			error = e instanceof Error ? e.message : 'Failed to load notes';
		} finally {
			loading = false;
		}
	}

	async function addNote() {
		if (!title.trim() || !description.trim()) return;

		try {
			loading = true;
			error = '';
			await api.createNote({ title, description });
			title = '';
			description = '';
			await loadNotes();
		} catch (e) {
			error = e instanceof Error ? e.message : 'Failed to create note';
		} finally {
			loading = false;
		}
	}

	async function deleteNote(id: number) {
		try {
			loading = true;
			error = '';
			await api.deleteNote(id);
			await loadNotes();
		} catch (e) {
			error = e instanceof Error ? e.message : 'Failed to delete note';
		} finally {
			loading = false;
		}
	}
</script>

<div class="container">
	<h1>📋 BulletBoard</h1>

	{#if error}
		<div class="error">{error}</div>
	{/if}

	<div class="note-form">
		<h2>Create New Note</h2>
		<input type="text" bind:value={title} placeholder="Title" disabled={loading} />
		<textarea bind:value={description} placeholder="Description" rows="4" disabled={loading}></textarea>
		<button on:click={addNote} disabled={loading || !title.trim() || !description.trim()}>
			{loading ? 'Adding...' : 'Add Note'}
		</button>
	</div>

	<div class="notes">
		<h2>Notes</h2>
		{#if loading && notes.length === 0}
			<p>Loading...</p>
		{:else if notes.length === 0}
			<p>No notes yet. Create one above!</p>
		{:else}
			{#each notes as note (note.id)}
				<div class="note-card">
					<h3>{note.title}</h3>
					<p>{note.content}</p>
					<div class="note-meta">
						<small>Created: {new Date(note.createdAt).toLocaleDateString()}</small>
						<button class="delete-btn" on:click={() => deleteNote(note.id)} disabled={loading}>
							Delete
						</button>
					</div>
				</div>
			{/each}
		{/if}
	</div>
</div>

<style>
	.container {
		max-width: 800px;
		margin: 0 auto;
		padding: 2rem;
	}

	h1 {
		color: #333;
		margin-bottom: 2rem;
	}

	.error {
		background: #fee;
		color: #c33;
		padding: 1rem;
		border-radius: 4px;
		margin-bottom: 1rem;
	}

	.note-form {
		background: #f5f5f5;
		padding: 1.5rem;
		border-radius: 8px;
		margin-bottom: 2rem;
	}

	.note-form h2 {
		margin-top: 0;
		color: #555;
	}

	input,
	textarea {
		width: 100%;
		padding: 0.75rem;
		margin-bottom: 1rem;
		border: 1px solid #ddd;
		border-radius: 4px;
		font-family: inherit;
		box-sizing: border-box;
	}

	button {
		background: #4CAF50;
		color: white;
		padding: 0.75rem 1.5rem;
		border: none;
		border-radius: 4px;
		cursor: pointer;
		font-size: 1rem;
	}

	button:hover:not(:disabled) {
		background: #45a049;
	}

	button:disabled {
		background: #ccc;
		cursor: not-allowed;
	}

	.notes h2 {
		color: #555;
	}

	.note-card {
		background: white;
		border: 1px solid #ddd;
		border-radius: 8px;
		padding: 1.5rem;
		margin-bottom: 1rem;
		box-shadow: 0 2px 4px rgba(0,0,0,0.1);
	}

	.note-card h3 {
		margin-top: 0;
		color: #333;
	}

	.note-meta {
		display: flex;
		justify-content: space-between;
		align-items: center;
		margin-top: 1rem;
		padding-top: 1rem;
		border-top: 1px solid #eee;
	}

	.delete-btn {
		background: #f44336;
		padding: 0.5rem 1rem;
		font-size: 0.9rem;
	}

	.delete-btn:hover:not(:disabled) {
		background: #da190b;
	}
</style>
