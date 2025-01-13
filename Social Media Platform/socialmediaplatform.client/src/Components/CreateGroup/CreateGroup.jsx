import React, { useState } from "react";
import '../JoinConversation/JoinConversation.css'

const CreateGroup = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [successMessage, setSuccessMessage] = useState(false);
    const [groupName, setGroupName] = useState("");

    const handleCreateGroup = async (e) => {
        e.preventDefault();

        if (!groupName.trim()) {
            setError("Group name is required.");
            return;
        }

        setLoading(true);
        setError(null);
        setSuccessMessage(false);

        const token = localStorage.getItem("token");

        try {
            const response = await fetch(`https://localhost:44354/conversation/create/group`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ name: groupName }),
            });

            if (response.ok) {
                setGroupName("");
                setSuccessMessage("Group created successfully!");
            } else {
                const responseData = await response.json();
                setError(responseData.error || "Failed to create group. Please try again.");
            }
        } catch (error) {
            console.error("Error creating group:", error);
            setError("An error occurred. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <section className="conversationsContainerMain">
            <div className="delimiter"></div>
            <div className="joinGroupContainer extra-delimiter">
                <header className="groupsHeader">
                    <h1 className="groupsLogoText">Create a Group</h1>
                </header>
                <p className="joinGroupAttentionMessage">
                    Want to create a new group? Enter the group name below.
                </p>
                <form onSubmit={handleCreateGroup} className="joinGroupForm">
                    <label htmlFor="groupName" className="joinGroupLabel">
                        Enter group name:
                    </label>
                    <textarea
                        id="groupName"
                        name="groupName"
                        className="joinGroupIdArea"
                        placeholder="Enter the name of the group..."
                        required
                        aria-label="Group Name"
                        value={groupName}
                        onChange={(e) => setGroupName(e.target.value)}
                    ></textarea>

                    <button type="submit" className="btn joinGroupButton" disabled={loading}>
                        {loading ? "Creating..." : "Create"}
                    </button>

                    {successMessage && !error && (
                        <p className="validMessage">{successMessage}</p>
                    )}
                    {error && <p className="errMessage">{error}</p>}
                </form>
            </div>
        </section>
    );
};

export default CreateGroup;
