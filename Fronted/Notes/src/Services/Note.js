import axios from "axios"

export const fetchNotes = async (filter) => {
    try {
        var response = await axios.get('http://localhost:5017/notes', {
            params: {
                search: filter?.search,
                sortOrder: filter?.sortOrder,
                sortItem: filter?.sortItem,
            },
        });

        return response.data.notes;
    } catch (e) {
        console.error(e);
    }
}

export const createNote = async (note) => {
    try {
        var response = await axios.post('http://localhost:5017/notes', note);

        return response.status;
    } catch (e) {
        console.error(e);
    }
}

export const updateNote = async (note) => {
    try {
        var response = await axios.put(`http://localhost:5017/notes/${note.id}`, note);

        return response.data;
    } catch (e) {
        console.error(e);
    }
}

export const deleteNote = async (id) => {
    try {
        var response = await axios.delete(`http://localhost:5017/notes/${id}`);

        return response.status;
    } catch (e) {
        console.error(e);
    }
}