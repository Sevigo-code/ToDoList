import { useState, useEffect } from 'react';
import type { ToDoItem, CreateToDoItem, UpdateToDoItem } from './types/ToDoItem';
import { todoService } from './services/api';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';
import './App.css';

function App() {
  const [items, setItems] = useState<ToDoItem[]>([]);
  const [editingItem, setEditingItem] = useState<ToDoItem | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchItems = async () => {
    try {
      setLoading(true);
      const response = await todoService.getAll();
      setItems(response.data);
      setError(null);
    } catch (err) {
      setError('Error al cargar las tareas. Esta corriendo el backend?');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchItems();
  }, []);

  const handleCreate = async (data: CreateToDoItem | UpdateToDoItem) => {
    try {
      await todoService.create(data as CreateToDoItem);
      fetchItems();
    } catch (err: unknown) {
      const axiosErr = err as { response?: { data?: { errors?: Record<string, string[]> } } };
      setError(axiosErr.response?.data?.errors
        ? Object.values(axiosErr.response.data.errors).flat().join(', ')
        : 'Error al crear la tarea.');
    }
  };

  const handleUpdate = async (data: CreateToDoItem | UpdateToDoItem) => {
    if (!editingItem) return;
    try {
      await todoService.update(editingItem.id, data as UpdateToDoItem);
      setEditingItem(null);
      fetchItems();
    } catch (err: unknown) {
      const axiosErr = err as { response?: { data?: { errors?: Record<string, string[]> } } };
      setError(axiosErr.response?.data?.errors
        ? Object.values(axiosErr.response.data.errors).flat().join(', ')
        : 'Error al actualizar la tarea.');
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('Estas seguro de eliminar esta tarea?')) return;
    try {
      await todoService.delete(id);
      fetchItems();
    } catch {
      setError('Error al eliminar la tarea.');
    }
  };

  const handleToggleComplete = async (item: ToDoItem) => {
    try {
      await todoService.update(item.id, {
        title: item.title,
        description: item.description,
        maxCompletionDate: item.maxCompletionDate,
        isCompleted: !item.isCompleted,
      });
      fetchItems();
    } catch {
      setError('Error al actualizar el estado.');
    }
  };

  return (
    <>
      <div className="app-header">
        <h1>Administrador de Tareas</h1>
        <p>Gestiona tus tareas pendientes</p>
      </div>

      {error && (
        <div className="alert alert-error">
          <span>{error}</span>
          <button onClick={() => setError(null)}>X</button>
        </div>
      )}

      <TodoForm
        onSubmit={editingItem ? handleUpdate : handleCreate}
        editingItem={editingItem}
        onCancel={() => setEditingItem(null)}
      />

      {loading ? (
        <div className="loading">Cargando...</div>
      ) : (
        <TodoList
          items={items}
          onEdit={setEditingItem}
          onDelete={handleDelete}
          onToggleComplete={handleToggleComplete}
        />
      )}
    </>
  );
}

export default App;
