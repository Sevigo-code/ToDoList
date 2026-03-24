import { useState, useEffect } from 'react';
import type { CreateToDoItem, ToDoItem, UpdateToDoItem } from '../types/ToDoItem';

interface Props {
  onSubmit: (data: CreateToDoItem | UpdateToDoItem) => void;
  editingItem?: ToDoItem | null;
  onCancel?: () => void;
}

export default function TodoForm({ onSubmit, editingItem, onCancel }: Props) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [maxCompletionDate, setMaxCompletionDate] = useState('');
  const [errors, setErrors] = useState<string[]>([]);

  useEffect(() => {
    if (editingItem) {
      setTitle(editingItem.title);
      setDescription(editingItem.description || '');
      setMaxCompletionDate(editingItem.maxCompletionDate.split('T')[0]);
    } else {
      setTitle('');
      setDescription('');
      setMaxCompletionDate('');
      setErrors([]);
    }
  }, [editingItem]);

  const validate = (): boolean => {
    const newErrors: string[] = [];
    if (!title.trim()) newErrors.push('El titulo es obligatorio.');
    if (title.length > 40) newErrors.push('El titulo no puede exceder 40 caracteres.');
    if (description.length > 200) newErrors.push('La descripcion no puede exceder 200 caracteres.');
    if (!maxCompletionDate) newErrors.push('La fecha maxima es obligatoria.');

    const today = new Date().toISOString().split('T')[0];
    if (maxCompletionDate < today) newErrors.push('La fecha debe ser hoy o futura.');

    setErrors(newErrors);
    return newErrors.length === 0;
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;

    if (editingItem) {
      onSubmit({
        title,
        description: description || undefined,
        maxCompletionDate,
        isCompleted: editingItem.isCompleted,
      } as UpdateToDoItem);
    } else {
      onSubmit({
        title,
        description: description || undefined,
        maxCompletionDate,
      } as CreateToDoItem);
    }

    setTitle('');
    setDescription('');
    setMaxCompletionDate('');
    setErrors([]);
  };

  return (
    <div className="card">
      <h3 className="card-title">{editingItem ? 'Editar Tarea' : 'Nueva Tarea'}</h3>
      <form onSubmit={handleSubmit}>
        <div className="form-grid">
          {errors.length > 0 && (
            <div className="form-errors">
              {errors.map((e, i) => <p key={i}>{e}</p>)}
            </div>
          )}

          <div className="form-group">
            <label>Titulo *</label>
            <input
              value={title}
              onChange={e => setTitle(e.target.value)}
              maxLength={40}
              placeholder="Nombre de la tarea"
            />
            <span className="char-count">{title.length}/40</span>
          </div>

          <div className="form-group">
            <label>Fecha limite *</label>
            <input
              type="date"
              value={maxCompletionDate}
              onChange={e => setMaxCompletionDate(e.target.value)}
              min={new Date().toISOString().split('T')[0]}
            />
          </div>

          <div className="form-group full-width">
            <label>Descripcion</label>
            <textarea
              value={description}
              onChange={e => setDescription(e.target.value)}
              maxLength={200}
              placeholder="Descripcion opcional"
              rows={2}
            />
            <span className="char-count">{description.length}/200</span>
          </div>

          <div className="form-actions">
            <button type="submit" className="btn btn-primary">
              {editingItem ? 'Actualizar' : 'Crear tarea'}
            </button>
            {editingItem && onCancel && (
              <button type="button" className="btn btn-secondary" onClick={onCancel}>
                Cancelar
              </button>
            )}
          </div>
        </div>
      </form>
    </div>
  );
}
