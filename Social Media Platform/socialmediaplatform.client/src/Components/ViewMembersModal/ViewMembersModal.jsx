import React from "react";
import './ViewMembersModal.css'
const ViewMembersModal = ({ isOpen, onClose, children }) => {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal">
                <button className="modal-close" onClick={onClose}>
                    &times;
                </button>
                <div className="modal-content-chat">{children}</div>
            </div>
        </div>
    );
};

export default ViewMembersModal;
