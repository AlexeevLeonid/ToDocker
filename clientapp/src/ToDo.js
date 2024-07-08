import React, { useState, useEffect } from 'react';
import axios from 'axios';
import API_URL from './config';

function ToDo() {
    const [todos, setTodos] = useState([]);
    const [newTodo, setNewTodo] = useState("");
    console.log(process.env.REACT_APP_API_URL)
    const api = axios.create({
        baseURL: API_URL,
      });

    useEffect(() => {
        fetchTodos();
    }, []);

    const fetchTodos = async () => {
        const response = await api.get('/api/TodoItems');
        setTodos(response.data);
    };

    const addTodo = async () => {
        if (newTodo) {
            const response = await api.post('/api/TodoItems', { title: newTodo, completed: false });
            setTodos([...todos, response.data]);
            setNewTodo("");
        }
    };

    const deleteTodo = async (id) => {
        await api.delete('/api/TodoItems/' + id);
        setTodos(todos.filter(todo => todo.id !== id));
    };

    return (
        <div>
            <h1>To-Do List</h1>
            <br></br>
            <input
                type="text"
                value={newTodo}
                onChange={(e) => setNewTodo(e.target.value)}
            />
            <button onClick={addTodo}>Add</button>
            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        {todo.title}
                        <button onClick={() => deleteTodo(todo.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default ToDo;
