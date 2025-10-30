import { PUBLIC_API_URL } from '$env/static/public';

const API_BASE = PUBLIC_API_URL || 'http://localhost:5000';

export interface Note {
	id: number;
	title: string;
	content: string;
	createdAt: string;
	updatedAt: string;
}

export const api = {
	async getNotes(): Promise<Note[]> {
		const response = await fetch(`${API_BASE}/notes`);
		if (!response.ok) throw new Error('Failed to fetch notes');
		return response.json();
	},

	async createNote(note: Omit<Note, 'id' | 'createdAt' | 'updatedAt'>): Promise<Note> {
		const response = await fetch(`${API_BASE}/notes`, {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify(note)
		});
		if (!response.ok) throw new Error('Failed to create note');
		return response.json();
	},

	async updateNote(id: number, note: Partial<Note>): Promise<Note> {
		const response = await fetch(`${API_BASE}/notes/${id}`, {
			method: 'PUT',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify(note)
		});
		if (!response.ok) throw new Error('Failed to update note');
		return response.json();
	},

	async deleteNote(id: number): Promise<void> {
		const response = await fetch(`${API_BASE}/notes/${id}`, {
			method: 'DELETE'
		});
		if (!response.ok) throw new Error('Failed to delete note');
	}
};
