using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F20SC_Browser {

    /// <summary>
    /// The short history is used for the session history.
    /// It's a simplified implementation of a doubly linked list.
    /// </summary>
    public class ShortHistory<T> {

        HistoryNode<T> shortHistory;

        public ShortHistory() {
            shortHistory = new HistoryNode<T>();
        }

        /// <summary>
        /// Adds a new next node and sets the head to the newly created node.
        /// </summary>
        /// <param name="url">URL of the new node head.</param>
        public void NewPageLoaded(T url) {
            shortHistory = shortHistory.AddNextNode(url);
        }

        /// <summary>
        /// Check if next page exists
        /// </summary>
        /// <returns>Bool to see if next node is null.</returns>
        public bool HasNextPage() {
            return shortHistory.nextNode != null;
        }

        /// <summary>
        /// Check if previous page exists
        /// </summary>
        /// <returns>Bool to see if previous node is null.</returns>
        public bool HasPrevPage() {

            if(shortHistory.prevNode != null) {
                return shortHistory.prevNode.nodeValue != null; 
            } else {
                return false;
            }

        }

        /// <summary>
        /// Traverses the short history backwards, returning the value of the new head.
        /// </summary>
        /// <returns>Returns value of T if short history has a previous node.</returns>
        public T TraverseBack() {
            if (HasPrevPage()) {
                shortHistory = shortHistory.prevNode;
                return shortHistory.nodeValue;
            } else {
                return default;
            }
        }

        /// <summary>
        /// Traverses the short history forward, returning the value of the new head.
        /// </summary>
        /// <returns>Returns value of T if short history has a next node.</returns>
        public T TraverseForward() {
            if (shortHistory.nextNode != null) {
                shortHistory = shortHistory.nextNode;
                return shortHistory.nodeValue;
            } else {
                return default;
            }
        }

        /// <summary>
        /// Gets the current value of the head.
        /// </summary>
        /// <returns>Returns the value of T of the current head.</returns>
        public T GetCurrentValue() {
            return shortHistory.nodeValue;
        }

    }

    class HistoryNode<T> {

        public HistoryNode<T> nextNode { get; private set; }
        public HistoryNode<T> prevNode { get; private set; }
        public T nodeValue { get; private set; }

        public HistoryNode() {
            prevNode = null;
            nextNode = null;
            nodeValue = default;
        }

        public HistoryNode(T nodeValue) {
            this.nodeValue = nodeValue;
            nextNode = null;
            prevNode = null;
        }

        public HistoryNode<T> AddNextNode(T url) {
            HistoryNode<T> newNode = new HistoryNode<T>(url);
            newNode.prevNode = this;
            nextNode = newNode;
            return newNode;
        }

    }

}
