import './JoinConversation.css';
import SVGgroupIcon from "@/Components/SVGs/SVGgroupIcon.jsx";
import { useState } from "react";

const JoinConversation = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [waitStatus, setWaitStatus] = useState(false);
    const [groupId, setGroupId] = useState("");

    const JoinGroupRequest = async (conversationId) => {
        setLoading(true);
        setError(null);
        setWaitStatus(false);

        try {
            const response = await fetch(`https://localhost:44354/conversation/join/${conversationId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
            });

            if (response.ok) {
                const data = await response.json();
            } else {
                const responseData = await response.json();
                setError(responseData.error || 'A conversation with this ID could not be found.');
            }
        } catch (e) {
            
        } finally {
            setLoading(false);
            if(!error)
                setWaitStatus(true);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!groupId.trim()) {
            setError("Group ID is required.");
            return;
        }

        setError(null); 
        setWaitStatus(false);
        await JoinGroupRequest(groupId);
    };

    return (
        <section className="conversationsContainer">
            <div className="delimiter"></div>
            <div className="joinGroupContainer">
                <header className="groupsHeader">
                    <SVGgroupIcon />
                    <h1 className="groupsLogoText">Groups</h1>
                </header>
                <p className="joinGroupAttentionMessage">
                    Are you looking to join a group?
                </p>
                <form onSubmit={handleSubmit} className="joinGroupForm">
                    <label htmlFor="groupId" className="joinGroupLabel">
                        Enter the group ID:
                    </label>
                    <textarea
                        id="groupId"
                        name="groupId"
                        className="joinGroupIdArea"
                        placeholder="Enter the ID of the group you want to join..."
                        required
                        aria-label="Group ID"
                        value={groupId}
                        onChange={(e) => setGroupId(e.target.value)}
                    ></textarea>

                    <button type="submit" className="joinGroupButton" disabled={loading}>
                        {loading ? "Joining..." : "Join Group"}
                    </button>

                    {waitStatus && !error && (
                        <p className="validMessage">Your request to join has been sent successfully.</p>
                    )}
                    {error && <p className="errMessage">{error}</p>}
                </form>
            </div>
        </section>
    );
};

export default JoinConversation;
