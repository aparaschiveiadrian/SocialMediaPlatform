import './Feed.css';
import React, {useEffect, useState} from 'react';

const Feed = () => {
    const [postList,setPostList] = useState([]);
    const fetchPosts = async () => {
        try {
            const response = await fetch('https://localhost:44354/posts');
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            setPostList(data);
        } catch (error) {
            console.error('Error fetching posts:', error);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []); // Runs only once when the component mounts
    console.log(postList)
    return (
        <div className="feed">
            <p>Fetching posts... Check the console for the output.</p>
        </div>
    );
};

export default Feed;
