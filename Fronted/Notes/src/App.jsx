import CreateNoteForm from './Components/CreateNoteForm';
import Note from './Components/Note';
import Filters from './Components/Filters';
import './App.css'
import { useEffect, useState } from 'react';
import { fetchNotes, createNote, updateNote, deleteNote } from './Services/Note';


export function App() {
  const [notes, setNotes] = useState([]);
  const [filter, setFilter] = useState({
    search: "",
    sortItem: "date",
    sortOrder: "desc",
  });

  const updateData = async () => {
    let notes = await fetchNotes(filter);
    setNotes(notes);
  }

  useEffect(() => {
    const fetchData = async () => {
      updateData()
    };

    fetchData();
  }, [filter]);

  const onCreate = async (note) => {
    await createNote(note);
    await updateData();
  }

  const onUpdate = async (note) => {
    await updateNote(note);
    await updateData();
  }

  const onDelete = async (id) => {
    await deleteNote(id);
    await updateData();
  }

  return (
    <section className='p-12 flex flex-row justify-around items-start'>
      <div className='flex flex-col w-1/3 gap-10'>
       <CreateNoteForm onCreate={onCreate} /> 
        <Filters filter={filter} setFilter={setFilter} />
      </div>

      <ul className='flex flex-col gap-5 w-1/2'>
        {notes.map((n) => (
          <li key={n.id}>
            <Note
              note = {n}
              onUpdate={onUpdate}
              onDelete={onDelete}
            />
          </li>
        ))}
      </ul>
    </section>
  )
}
