import type { ToDoItem } from '../types/ToDoItem';

interface Props {
  items: ToDoItem[];
  onEdit: (item: ToDoItem) => void;
  onDelete: (id: number) => void;
  onToggleComplete: (item: ToDoItem) => void;
}

export default function TodoList({ items, onEdit, onDelete, onToggleComplete }: Props) {
  if (items.length === 0) {
    return (
      <div className="card">
        <p className="task-empty">No hay tareas registradas. Crea una nueva tarea para comenzar.</p>
      </div>
    );
  }

  const pending = items.filter(i => !i.isCompleted).length;

  return (
    <div className="card">
      <div className="task-list-header">
        <h2>Tareas</h2>
        <span className="task-count">{pending} pendiente{pending !== 1 ? 's' : ''} de {items.length}</span>
      </div>

      {items.map(item => (
        <div key={item.id} className={`task-item${item.isCompleted ? ' completed' : ''}`}>
          <input
            type="checkbox"
            className="task-checkbox"
            checked={item.isCompleted}
            onChange={() => onToggleComplete(item)}
          />

          <div className="task-content">
            <div className="task-title">{item.title}</div>
            {item.description && (
              <div className="task-description">{item.description}</div>
            )}
            <div className="task-meta">
              <span>Limite: {new Date(item.maxCompletionDate).toLocaleDateString()}</span>
              <span>Creada: {new Date(item.createdAt).toLocaleDateString()}</span>
            </div>
          </div>

          <div className="task-actions">
            <button className="btn btn-ghost" onClick={() => onEdit(item)}>Editar</button>
            <button className="btn btn-danger" onClick={() => onDelete(item.id)}>Eliminar</button>
          </div>
        </div>
      ))}
    </div>
  );
}
